using Microsoft.Extensions.DependencyInjection;

namespace FirstRatePlus.LoggingTelemetry.Infrastructure;

public static class StartupSetup
{
  public static void AddCosmosRepo(this IServiceCollection services) =>
      services.AddCosmosRepository(); // will be created in web project root
}
