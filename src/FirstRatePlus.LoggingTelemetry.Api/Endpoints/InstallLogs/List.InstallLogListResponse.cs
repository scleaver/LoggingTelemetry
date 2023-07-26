using FirstRatePlus.LoggingTelemetry.Api.ApiModels;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

/// <summary>
/// Represents an install log.
/// </summary>
public class InstallLogListResponse
{
  /// <summary>
  /// The ID of the install log item.
  /// </summary>
  public string? Id { get; set; }

  /// <summary>
  /// The ID of the user installing the software.
  /// </summary>
  public string UserId { get; set; } = null!;

  /// <summary>
  /// The ID of the machine where the software is being installed.
  /// </summary>
  public string MachineId { get; set; } = null!;

  /// <summary>
  /// The official release number of the software when the record was created. eg. 53000
  /// </summary>
  public int ReleaseNumber { get; set; }

  /// <summary>
  /// The patch version of the software when the record was created. eg. 53000
  /// </summary>
  public string PatchVersion { get; set; } = null!;

  /// <summary>
  /// The name of the software being installed when the record was created.
  /// </summary>
  public string SoftwareName { get; set; } = null!;

  /// <summary>
  /// The date and time the record was created in UTC.
  /// </summary>
  public DateTime DateCreatedUtc { get; set; }
}
