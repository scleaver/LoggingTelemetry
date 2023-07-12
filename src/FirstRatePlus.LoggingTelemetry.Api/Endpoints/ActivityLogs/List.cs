﻿using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.ApiModels;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

public class List : Endpoint<ActivityLogListRequest, PagedResponse<ActivityLogListResponse>>
{
  private readonly IRepository<ActivityLog> _repository;

  public List(IRepository<ActivityLog> repository)
  {
    _repository = repository;
  }

  /// <summary>
  /// Configures the endpoint.
  /// </summary>
  public override void Configure()
  {
    Get(ActivityLogListRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ActivityLog"));
  }

  public override async Task HandleAsync(ActivityLogListRequest req, CancellationToken ct)
  {
    if (req.ToUtc is null)
    {
      req.ToUtc = DateTime.UtcNow;
    }

    if (req.FromUtc is null)
    {
      req.FromUtc = req.ToUtc.Value.AddDays(-30);
    }

    var pagedResults = await _repository.PageAsync(i => i.DateCreatedUtc >= req.FromUtc && i.DateCreatedUtc <= req.ToUtc, req.Page, req.PageSize, true, ct);

    var response = new PagedResponse<ActivityLogListResponse>(new(), 0, req.Page, req.PageSize);

    if (pagedResults is not null)
    {
      var mapper = new ActivityLogMapper();

      var entities = pagedResults.Items.Select(i => mapper.ActivityLogToActivityLogListResponse(i)).ToList();

      response = new PagedResponse<ActivityLogListResponse>(entities, pagedResults.Total ?? 0, pagedResults.PageNumber ?? 1, pagedResults.Size);
    }

    await SendAsync(response, cancellation: ct);
    return;
  }
}

