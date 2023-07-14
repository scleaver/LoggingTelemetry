using Ardalis.Specification;
using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.ApiModels;
using FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogEndpoints;

/// <summary>
/// Get a list of install logs.
/// </summary>
public class List : Endpoint<InstallLogListRequest, PagedResponse<InstallLogListResponse>>
{
  private readonly IRepository<InstallLog> _repository;

  public List(IRepository<InstallLog> repository)
  {
    _repository = repository;
  }

  /// <summary>
  /// Configures the endpoint.
  /// </summary>
  public override void Configure()
  {
    Get(InstallLogListRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(InstallLogListRequest req, CancellationToken ct)
  {
    if (req.DateTo is null)
    {
      req.DateTo = DateTime.UtcNow;
    }

    if (req.DateFrom is null)
    {
      req.DateFrom = req.DateTo.Value.AddDays(-30);
    }

    var pagedResults = await _repository.PageAsync(i => i.DateCreatedUtc >= req.DateFrom && i.DateCreatedUtc <= req.DateTo, req.Page, req.PageSize, true, ct);

    var response = new PagedResponse<InstallLogListResponse>(new(), 0, req.Page, req.PageSize);

    if (pagedResults is not null)
    {
      var mapper = new InstallLogMapper();

      var entities = pagedResults.Items.Select(i => mapper.ToInstallListLogResponse(i)).ToList();

      response = new PagedResponse<InstallLogListResponse>(entities, pagedResults.Total ?? 0, pagedResults.PageNumber ?? 1, pagedResults.Size);
    }

    await SendAsync(response, cancellation: ct);
    return;
  }
}