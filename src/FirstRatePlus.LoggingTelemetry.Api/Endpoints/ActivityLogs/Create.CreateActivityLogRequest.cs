using System.ComponentModel.DataAnnotations;
using FirstRatePlus.LoggingTelemetry.Api.Constants;
using Newtonsoft.Json.Linq;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

public class CreateActivityLogRequest
{
  public const string Route = Routes.ActivityLogs;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.UserId"/>
  [Required]
  public string UserId { get; set; } = null!;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.ActivityType"/>
  [Required]
  public string ActivityType { get; set; } = null!;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.ReleaseNumber"/>
  [Required]
  public int ReleaseNumber { get; set; }

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.ActivityDateUtc"/>
  [Required]
  public DateTime ActivityDateUtc { get; }

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.SoftwareName"/>
  [Required]
  public string SoftwareName { get; set; } = null!;

  /// <inheritdoc cref="Core.Aggregates.ActivityLog.Data"/>
  [Required]
  public JObject Data { get; set; }

  public CreateActivityLogRequest()
  {
    // Initialize the dynamic JSON object
    Data = new JObject();
  }
}

