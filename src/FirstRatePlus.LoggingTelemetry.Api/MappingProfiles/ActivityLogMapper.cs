using FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Riok.Mapperly.Abstractions;

namespace FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;

[Mapper]
public partial class ActivityLogMapper
{
  [MapProperty(nameof(CreateActivityLogRequest.ActivityDate), nameof(ActivityLog.ActivityDateUtc))]
  public partial ActivityLog FromCreateActivityLogRequest(CreateActivityLogRequest request);

  public partial GetActivityLogResponse ToGetActivityLogResponse(ActivityLog activityLog);

  public partial ActivityLogListResponse ToActivityLogListResponse(ActivityLog activityLog);

  /// <summary>
  /// Maps DateTimeOffset to DateTime for UTC conversion.
  /// </summary>
  private DateTime DateTimeOffsetToDateTime(DateTimeOffset t) => t.UtcDateTime;
}
