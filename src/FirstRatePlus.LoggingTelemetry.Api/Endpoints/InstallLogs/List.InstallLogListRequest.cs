﻿using System;
using FirstRatePlus.LoggingTelemetry.Api.ApiModels;
using FirstRatePlus.LoggingTelemetry.Api.Constants;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

public class InstallLogListRequest : PagedRequest
{
  public const string Route = Routes.InstallLogs;

  /// <summary>
  /// The 
  /// </summary>
  public DateTime? FromUtc { get; set; }

  /// <summary>
  /// 
  /// </summary>
  public DateTime? ToUtc { get; set; }

}
