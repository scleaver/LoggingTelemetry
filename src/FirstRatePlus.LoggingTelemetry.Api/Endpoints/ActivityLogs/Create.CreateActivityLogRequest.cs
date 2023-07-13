using System.ComponentModel.DataAnnotations;
using FirstRatePlus.LoggingTelemetry.Api.Constants;
using Newtonsoft.Json.Linq;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

public class CreateActivityLogRequest
{
  public const string Route = Routes.ActivityLogs;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.UserId"/>
  [Required]
  public string UserId { get; set; } = null!;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.ActivityType"/>
  [Required]
  public string ActivityType { get; set; } = null!;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.ReleaseNumber"/>
  [Required]
  public int ReleaseNumber { get; set; }

  /// <summary>
  /// The date and time the activity was initiated in ISO 8601 format. eg. 2023-07-13T12:00:00+10:00 or 2023-07-13T02:00:00Z
  /// </summary>
  [Required]
  public DateTimeOffset ActivityDate { get; set; }

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.SoftwareName"/>
  [Required]
  public string SoftwareName { get; set; } = null!;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.Data"/>
  [Required]
  public JObject Data { get; set; }

  public CreateActivityLogRequest()
  {
    // Initialize the dynamic JSON object
    Data = new JObject();
  }
}

