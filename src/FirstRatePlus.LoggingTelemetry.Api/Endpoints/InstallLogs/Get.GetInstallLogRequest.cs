using System.ComponentModel.DataAnnotations;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

public class GetInstallLogRequest
{
  public const string Route = "internal/logging/install-logs/{Id}";
  public static string BuildRoute(string id) => Route.Replace("{Id}", id);

  [Required]
  public string Id { get; set; } = null!;
}
