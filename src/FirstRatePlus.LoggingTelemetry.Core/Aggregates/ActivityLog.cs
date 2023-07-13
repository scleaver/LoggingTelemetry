using Microsoft.Azure.CosmosRepository;
using Newtonsoft.Json.Linq;

namespace FirstRatePlus.LoggingTelemetry.Core.Aggregates;

/// <summary>
/// Represents a log of an activity.
/// </summary>
public class ActivityLog : Item
{
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
  /// The type of activity this log represents.
  /// </summary>
  public string ActivityType { get; set; } = null!;

  /// <summary>
  /// The date and time the activity log record was created in UTC.
  /// </summary>
  public DateTime DateCreatedUtc { get; private set; }

  /// <summary>
  /// The date and time the activity was initiated in UTC.
  /// </summary>
  public DateTime ActivityDateUtc { get; set; }

  /// <summary>
  /// The dynamic JSON object associated with the activity log.
  /// </summary>
  public JObject Data { get; set; }

  public ActivityLog()
  {
    // The date the log is created.
    DateCreatedUtc = DateTime.UtcNow;

    // Initialize the dynamic JSON object
    Data = new JObject();
  }
}
