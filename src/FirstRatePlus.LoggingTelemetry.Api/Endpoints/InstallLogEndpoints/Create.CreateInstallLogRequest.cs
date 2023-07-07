using System;
using System.ComponentModel.DataAnnotations;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogEndpoints;

public class CreateInstallLogRequest
{
  public const string Route = "internal/logging/install-logs";

  [Required]
  public string? UserId { get; set; }

  [Required]
  public string? MachineId { get; set; }

  [Required]
  public int ReleaseNumber { get; set; }

  [Required]
  public string? SoftwareName { get; set; }
}
