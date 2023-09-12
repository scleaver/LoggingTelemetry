using FirstRatePlus.LoggingTelemetry.Api;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.CosmosRepository;
using Autofac.Core;

namespace FirstRatePlus.LoggingTelemetry.FunctionalTests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
  /// <summary>
  /// Overriding CreateHost to avoid creating a separate ServiceProvider per this thread:
  /// https://github.com/dotnet-architecture/eShopOnWeb/issues/465
  /// </summary>
  /// <param name="builder"></param>
  /// <returns></returns>
  protected override IHost CreateHost(IHostBuilder builder)
  {
    builder.UseEnvironment("Development"); // will not send real emails
    var host = builder.Build();
    host.Start();

    // Get service provider.
    var serviceProvider = host.Services;

    // Create a scope to obtain a reference to the database
    // context (AppDbContext).
    using (var scope = serviceProvider.CreateScope())
    {
      var scopedServices = scope.ServiceProvider;
      IRepositoryFactory factory = scopedServices.GetRequiredService<IRepositoryFactory>()!;

      var logger = scopedServices
          .GetRequiredService<ILogger<CustomWebApplicationFactory<TProgram>>>();

      // Ensure the database is created.
      //db.Database.EnsureCreated();

      try
      {
        // Can also skip creating the items
        //if (!db.ToDoItems.Any())
        //{
        // Seed the database with test data.
        SeedData.InitializeAsync(factory).Wait();
        //}
      }
      catch (Exception ex)
      {
        logger.LogError(ex, "An error occurred seeding the DB. Error: {exceptionMessage}", ex.Message);
      }
    }

    return host;
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder
        .ConfigureServices(services =>
        {
          //// Remove the app's ApplicationDbContext registration.
          //var descriptor = services.SingleOrDefault(
          //d => d.ServiceType ==
          //    typeof(DbContextOptions<AppDbContext>));

          //if (descriptor != null)
          //{
          //  services.Remove(descriptor);
          //}

          //// This should be set for each individual test run
          //string inMemoryCollectionName = Guid.NewGuid().ToString();

          //// Add ApplicationDbContext using an in-memory database for testing.
          //services.AddDbContext<AppDbContext>(options =>
          //{
          //  options.UseInMemoryDatabase(inMemoryCollectionName);
          //});
        });
  }
}
