using Newtonsoft.Json.Linq;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

public class GetActivityLogResponse
{
  /// <summary>
  /// The ID of the activity.
  /// </summary>
  public string Id { get; set; } = null!;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.UserId"/>
  public string UserId { get; set; } = null!;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.ReleaseNumber"/>
  public int ReleaseNumber { get; set; }

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.SoftwareName"/>
  public string SoftwareName { get; set; } = null!;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.ActivityType"/>
  public string ActivityType { get; set; } = null!;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.DateCreatedUtc"/>
  public DateTime DateCreatedUtc { get; set; }

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.ActivityDateUtc"/>
  public DateTime ActivityDateUtc { get; set; }

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.Data/>
  public JObject Data { get; set; }

  public GetActivityLogResponse()
  {
    // Initialize the dynamic JSON object
    Data = new JObject();
  }
}

