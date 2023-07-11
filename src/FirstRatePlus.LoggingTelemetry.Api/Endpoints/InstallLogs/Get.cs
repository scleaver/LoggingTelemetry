using System.Net;
using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

public class Get : Endpoint<GetInstallLogRequest, GetInstallLogResponse>
{
  private readonly IRepository<InstallLog> _repository;

  public Get(IRepository<InstallLog> repository)
  {
    _repository = repository;
  }

  /// <summary>
  /// Configures the endpoint.
  /// </summary>
  public override void Configure()
  {
    Get(GetInstallLogRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("InstallLog"));
  }

  public override async Task HandleAsync(GetInstallLogRequest req, CancellationToken ct)
  {
    if (string.IsNullOrEmpty(req.Id))
    {
      ThrowError("Id is required.");
    }

    try
    {
      InstallLog entity = await _repository.GetAsync(req.Id, null, ct);

      InstallLogMapper mapper = new InstallLogMapper();

      GetInstallLogResponse response = mapper.FromInstallLogToGetInstallLogResponse(entity);

      await SendAsync(response, cancellation: ct);

    }
    catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
    {
      await SendNotFoundAsync(ct);
      return;
    }
  }
}

