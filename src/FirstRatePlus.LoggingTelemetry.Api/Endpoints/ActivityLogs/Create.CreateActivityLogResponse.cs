using System;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

public class CreateActivityLogResponse
{
  /// <summary>
  /// The ID of the activity.
  /// </summary>
  public string Id { get; set; } = null!;
}
