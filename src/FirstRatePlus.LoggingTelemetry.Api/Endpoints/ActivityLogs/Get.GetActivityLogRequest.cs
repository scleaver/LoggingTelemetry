using System;
using System.ComponentModel.DataAnnotations;
using FirstRatePlus.LoggingTelemetry.Api.Constants;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

public class GetActivityLogRequest
{
  public const string Route = Routes.ActivityLogs + "/{Id}";
  public static string BuildRoute(string id) => Route.Replace("{Id}", id);

  /// <summary>
  /// The ID of the activity.
  /// </summary>
  [Required]
  public string Id { get; set; } = null!;
}

