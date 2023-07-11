using FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;
using FirstRatePlus.LoggingTelemetry.Core.Entities;
using Riok.Mapperly.Abstractions;

namespace FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;

[Mapper]
public partial class InstallLogMapper
{
  public partial InstallLog CreateInstallLogRequestToInstallLog(CreateInstallLogRequest request);
  public partial CreateInstallLogResponse FromInstallLogToCreateInstallLogResponse(InstallLog installLog);
  public partial GetInstallLogResponse FromInstallLogToGetInstallLogResponse(InstallLog installLog);
  public partial InstallLogListResponse FromInstallLogToInstallListLogResponse(InstallLog installLog);
}