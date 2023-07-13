using System;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

public class CreateActivityLogResponse
{
  /// <summary>
  /// The ID of the activity log.
  /// </summary>
  public string Id { get; set; } = null!;
}
