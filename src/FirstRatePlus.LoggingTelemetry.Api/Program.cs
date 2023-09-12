using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FastEndpoints;
using FastEndpoints.Swagger;
using FirstRatePlus.LoggingTelemetry.Api;
using FirstRatePlus.LoggingTelemetry.Core;
using FirstRatePlus.LoggingTelemetry.Infrastructure;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.AddCosmosRepo();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(o =>
{
  o.DocumentSettings = s =>
  {
    s.Title = "FirstRatePlus Logging and Telemetry";
    s.Version = "v1";
  };
  o.ShortSchemaNames = true;
  o.AutoTagPathSegmentIndex = 3;
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});

builder.Services.AddHealthChecks()
  .AddCosmosDb(builder.Configuration.GetCosmosRepositoryConnectionString()!)
  .AddApplicationInsightsPublisher();

builder.Services.AddApplicationInsightsTelemetry();

builder.Logging.AddAzureWebAppDiagnostics(); //add this if deploying to Azure

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();

  // Seed Database
  using (var scope = app.Services.CreateScope())
  {
    var services = scope.ServiceProvider;

    try
    {
      IRepositoryFactory factory = services.GetRequiredService<IRepositoryFactory>()!;

      await SeedData.InitializeAsync(factory);
    }
    catch (Exception ex)
    {
      var logger = services.GetRequiredService<ILogger<Program>>();
      logger.LogError(ex, "An error occurred seeding the DB. Error: {exceptionMessage}", ex.Message);
    }
  }
}
else
{
  app.UseExceptionHandler("/Error");
  app.UseHsts();
}
//app.UseRouting();
app.UseFastEndpoints(c =>
{
  c.Serializer.RequestDeserializer = async (req, tDto, jCtx, ct) =>
  {
    using var reader = new StreamReader(req.Body);
    return JsonConvert.DeserializeObject(await reader.ReadToEndAsync(), tDto);
  };

  DefaultContractResolver contractResolver = new DefaultContractResolver
  {
    NamingStrategy = new CamelCaseNamingStrategy()
  };

  c.Serializer.ResponseSerializer = (rsp, dto, cType, jCtx, ct) =>
  {
    rsp.ContentType = cType;
    return rsp.WriteAsync(JsonConvert.SerializeObject(dto, new JsonSerializerSettings
    {
      ContractResolver = contractResolver
    }), ct);
  };
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapHealthChecks("/healthz");

//// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwaggerGen();

//// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
//app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FirstRatePlus Logging and Telemetry V1"));

app.Run();

// Make the implicit Program.cs class public, so integration tests can reference the correct assembly for host building
public partial class Program
{
}
