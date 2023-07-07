using System;
namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogEndpoints;

public class CreateInstallLogResponse
{
  public string InstallId { get; set; } = null!;
  public string UserId { get; set; } = null!;
  public string MachineId { get; set; } = null!;
  public int ReleaseNumber { get; set; }
  public string SoftwareName { get; set; } = null!;
  public DateTime DateCreatedUtc { get; set; }
}
