using System.ComponentModel.DataAnnotations;
using FirstRatePlus.LoggingTelemetry.Api.Constants;
using Newtonsoft.Json.Linq;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

/// <summary>
/// Represents data about an activity.
/// </summary>
public class CreateActivityLogRequest
{
  public const string Route = Routes.ActivityLogs;

  /// <summary>
  /// The ID of the user who initiated the activity.
  /// </summary>
  [Required]
  public string UserId { get; set; } = null!;

  /// <summary>
  /// The type of activity eg. 'Calculation'.
  /// </summary>
  [Required]
  public string ActivityType { get; set; } = null!;

  /// <summary>
  /// The official release number of the software where the activity occured. eg. 53000
  /// </summary>
  [Required]
  public int ReleaseNumber { get; set; }

  /// <summary>
  /// The date and time the activity was initiated in ISO 8601 format. eg. 2023-07-13T12:00:00+10:00 or 2023-07-13T02:00:00Z
  /// This will be converted to the corresponding date and time in UTC. 
  /// </summary>
  [Required]
  public DateTimeOffset ActivityDate { get; set; }

  /// <summary>
  /// The name of the software where the activity occured.
  /// </summary>
  [Required]
  public string SoftwareName { get; set; } = null!;

  /// <summary>
  /// An optional JSON object with additional information to be recorded against the activity.
  /// </summary>
  public JObject Data { get; set; }

  public CreateActivityLogRequest()
  {
    // Initialize the dynamic JSON object
    Data = new JObject();
  }
}

