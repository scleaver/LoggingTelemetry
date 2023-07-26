/*
 * Get.cs
 * 
 * This class represents an API endpoint for getting an install log.
 * It inherits from the Endpoint class provided by the FastEndpoints library.
 * The endpoint handles HTTP GET requests to the specified route and allows anonymous access.
 * It uses the Mapperly library for object mapping and the Microsoft.Azure.CosmosRepository library for data access.
 */

using System.Net;
using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;

/// <summary>
/// Get an install log.
/// </summary>
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

      GetInstallLogResponse response = mapper.ToGetInstallLogResponse(entity);

      await SendAsync(response, cancellation: ct);

    }
    catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
    {
      await SendNotFoundAsync(ct);
      return;
    }
  }
}

