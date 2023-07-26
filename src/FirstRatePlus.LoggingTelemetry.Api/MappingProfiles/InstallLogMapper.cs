using FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Riok.Mapperly.Abstractions;

namespace FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;

[Mapper]
public partial class InstallLogMapper
{
  public partial InstallLog FromCreateInstallLogRequest(CreateInstallLogRequest request);
  public partial GetInstallLogResponse ToGetInstallLogResponse(InstallLog installLog);
  public partial InstallLogListResponse ToInstallListLogResponse(InstallLog installLog);
}
