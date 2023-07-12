using System.ComponentModel.DataAnnotations;
using FirstRatePlus.LoggingTelemetry.Api.Constants;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

public class GetInstallLogRequest
{
  public const string Route = Routes.InstallLogs + "/{Id}";
  public static string BuildRoute(string id) => Route.Replace("{Id}", id);

  [Required]
  public string Id { get; set; } = null!;
}
