using System;
using FirstRatePlus.LoggingTelemetry.Api.ApiModels;
using FirstRatePlus.LoggingTelemetry.Api.Constants;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

public class ActivityLogListRequest : PagedRequest
{
  public const string Route = Routes.ActivityLogs;

  /// <summary>
  /// The 
  /// </summary>
  public DateTime? FromUtc { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public DateTime? ToUtc { get; set; }

}
