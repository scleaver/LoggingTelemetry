using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Microsoft.Extensions.DependencyInjection;

namespace FirstRatePlus.LoggingTelemetry.Infrastructure;

public static class StartupSetup
{
  public static void AddCosmosRepo(this IServiceCollection services) =>
      services.AddCosmosRepository(options =>
      {
        options.ContainerPerItemType = true;
        options.OptimizeBandwidth = true;

        options.ContainerBuilder.Configure<ActivityLog>(builder =>
          builder.WithServerlessThroughput());

        options.ContainerBuilder.Configure<InstallLog>(builder =>
          builder.WithServerlessThroughput());
      }); // will be created in web project root
}
