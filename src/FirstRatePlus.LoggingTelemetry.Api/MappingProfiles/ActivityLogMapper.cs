using FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Riok.Mapperly.Abstractions;

namespace FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;

[Mapper]
public partial class ActivityLogMapper
{
  public partial ActivityLog RequestToActivityLog(CreateActivityLogRequest request);
  public partial GetActivityLogResponse ActivityLogToGetActivityLogResponse(ActivityLog activityLog);
  public partial ActivityLogListResponse ActivityLogToActivityLogListResponse(ActivityLog activityLog);
}
