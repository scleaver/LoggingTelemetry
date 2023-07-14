using FirstRatePlus.LoggingTelemetry.Api.ApiModels;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

/// <summary>
/// Represents an install log.
/// </summary>
public class InstallLogListResponse
{
  /// <summary>
  /// The ID of the install.
  /// </summary>
  public string Id { get; set; } = null!;

  /// <summary>
  /// The ID of the user who initiated the install.
  /// </summary>
  public string UserId { get; set; } = null!;


  public string MachineId { get; set; } = null!;

  /// <summary>
  /// The official release number of the software when the install occured. eg. 53000
  /// </summary>
  public int ReleaseNumber { get; set; }

  /// <summary>
  /// The name of the software that the install relates to.
  /// </summary>
  public string SoftwareName { get; set; } = null!;

  /// <summary>
  /// The date and time the install log record was created in UTC.
  /// </summary>
  public DateTime DateCreatedUtc { get; set; }
}
