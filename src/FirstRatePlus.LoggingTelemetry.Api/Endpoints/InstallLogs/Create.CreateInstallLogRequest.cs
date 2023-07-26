using System.ComponentModel.DataAnnotations;
using FirstRatePlus.LoggingTelemetry.Api.Constants;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

public class CreateInstallLogRequest
{
  public const string Route = Routes.InstallLogs;

  /// <summary>
  /// The ID of the user installing the software.
  /// </summary>
  [Required]
  public string UserId { get; set; } = null!;

  /// <summary>
  /// The ID of the machine where the software is being installed.
  /// </summary>
  [Required]
  public string MachineId { get; set; } = null!;

  /// <summary>
  /// The official release number of the software when the record was created. eg. 53000
  /// </summary>
  [Required]
  public int ReleaseNumber { get; set; }

  /// <summary>
  /// The patch version of the software when the record was created. eg. 53000
  /// </summary>
  public string PatchVersion { get; set; } = null!;

  /// <summary>
  /// The name of the software being installed when the record was created.
  /// </summary>
  [Required]
  public string SoftwareName { get; set; } = null!;
}
