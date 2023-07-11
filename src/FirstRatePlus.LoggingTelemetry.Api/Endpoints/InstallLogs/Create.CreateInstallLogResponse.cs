namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

public class CreateInstallLogResponse
{
  /// <inheritdoc cref="InstallLog.Id"/>
  public string? Id { get; set; }

  /// <inheritdoc cref="InstallLog.UserId"/>
  public string? UserId { get; set; }

  public string? MachineId { get; set; }


  public int ReleaseNumber { get; set; }
  public string? SoftwareName { get; set; }
  public DateTime DateCreatedUtc { get; set; }
}
