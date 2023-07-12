using System.ComponentModel.DataAnnotations;
using FirstRatePlus.LoggingTelemetry.Api.Constants;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

public class CreateInstallLogRequest
{
  public const string Route = Routes.InstallLogs;

  /// <inheritdoc cref="Core.Aggregates.InstallLog.UserId"/>
  [Required]
  public string UserId { get; set; } = null!;

  /// <summary>
  /// The ID of the machine where the software is being installed.
  /// </summary>
  [Required]
  public string MachineId { get; set; } = null!;

  /// <summary>
  /// The official release number.
  /// </summary>
  [Required]
  public int ReleaseNumber { get; set; }

  [Required]
  public string SoftwareName { get; set; } = null!;
}
