using Microsoft.Extensions.DependencyInjection;

namespace FirstRatePlus.LoggingTelemetry.Infrastructure;

public static class StartupSetup
{
  public static void AddCosmosRepo(this IServiceCollection services) =>
      services.AddCosmosRepository(options =>
      {
        options.ContainerPerItemType = true;
        options.OptimizeBandwidth = true;
      }); // will be created in web project root
}
