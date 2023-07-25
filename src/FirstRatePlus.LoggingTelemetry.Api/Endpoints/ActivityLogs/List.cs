using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.ApiModels;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

/// <summary>
/// Get a list of activity logs.
/// </summary>
/// <remarks></remarks>
public class List : Endpoint<ActivityLogListRequest, PagedResponse<ActivityLogListResponse>>
{
  private readonly IRepository<ActivityLog> _repository;

  public List(IRepository<ActivityLog> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get(ActivityLogListRequest.Route);
    AllowAnonymous();
  }

  public override async Task HandleAsync(ActivityLogListRequest req, CancellationToken ct)
  {

    if (req.DateTo is null)
    {
      req.DateTo = DateTime.UtcNow;
    }

    if (req.DateFrom is null)
    {
      req.DateFrom = req.DateTo.Value.AddDays(-30);
    }

    var pagedResults = await _repository.PageAsync(i => i.ActivityDateUtc >= req.DateFrom && i.ActivityDateUtc <= req.DateTo, req.Page, req.PageSize, true, ct);

    var response = new PagedResponse<ActivityLogListResponse>(new(), 0, req.Page, req.PageSize);

    if (pagedResults is not null)
    {
      var mapper = new ActivityLogMapper();

      var entities = pagedResults.Items.Select(i => mapper.ToActivityLogListResponse(i)).ToList();

      response = new PagedResponse<ActivityLogListResponse>(entities, pagedResults.Total ?? 0, pagedResults.PageNumber ?? 1, pagedResults.Size);
    }

    await SendAsync(response, cancellation: ct);
    return;
  }
}

