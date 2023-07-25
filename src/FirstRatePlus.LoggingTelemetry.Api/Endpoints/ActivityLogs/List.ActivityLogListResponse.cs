using Newtonsoft.Json.Linq;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

/// <summary>
/// Represents an activity log.
/// </summary>
public class ActivityLogListResponse
{
  /// <summary>
  /// The ID of the activity.
  /// </summary>
  public string Id { get; set; } = null!;

  /// <summary>
  /// The ID of the user who initiated the activity.
  /// </summary>
  public string UserId { get; set; } = null!;

  /// <summary>
  /// The official release number of the software when the activity occured. eg. 53000
  /// </summary>
  public int ReleaseNumber { get; set; }

  /// <summary>
  /// The name of the software that the activity relates to.
  /// </summary>
  public string SoftwareName { get; set; } = null!;

  /// <summary>
  /// The date and time the activity log record was created in UTC.
  /// </summary>
  public DateTime DateCreatedUtc { get; set; }

  /// <summary>
  /// The date and time the activity was initiated in UTC.
  /// </summary>
  public DateTime ActivityDateUtc { get; set; }

  /// <summary>
  /// An optional JSON object with additional information to be recorded against the activity.
  /// </summary>
  public JObject Data { get; set; }

  public ActivityLogListResponse()
  {
    // Initialize the dynamic JSON object
    Data = new();
  }
}
