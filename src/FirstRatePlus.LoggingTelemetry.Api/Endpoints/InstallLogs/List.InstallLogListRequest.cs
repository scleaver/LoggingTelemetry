using System;
using FirstRatePlus.LoggingTelemetry.Api.ApiModels;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

public class InstallLogListRequest : PagedRequest
{
  public const string Route = "internal/logging/install-logs";

  /// <summary>
  /// The 
  /// </summary>
  public DateTime? FromUtc { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public DateTime? ToUtc { get; set; }

}
