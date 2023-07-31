using FirstRatePlus.LoggingTelemetry.Api.Constants;
using FirstRatePlus.SharedApplication.ApiModels;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

public class InstallLogListRequest : PagedRequest
{
  public const string Route = Routes.InstallLogs;

  /// <summary>
  /// The earliest install date/time for which to fetch the results in ISO 8601 format. eg. '2023-07-13T12:00:00+10:00' or '2023-07-13T02:00:00Z'. This defaults to 30 days from the current date and time.
  /// </summary>
  public DateTime? DateFrom { get; set; }

  /// <summary>
  /// The latest install date/time for which to fetch the results in ISO 8601 format. eg. '2023-07-13T12:00:00+10:00' or '2023-07-13T02:00:00Z'. This defaults to the current date and time.
  /// </summary>
  public DateTime? DateTo { get; set; }

}
