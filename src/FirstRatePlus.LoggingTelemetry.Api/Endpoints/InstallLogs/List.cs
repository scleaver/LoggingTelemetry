﻿using Ardalis.Specification;
using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.ApiModels;
using FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Entities;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogEndpoints;

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
    Options(x => x
      .WithTags("InstallLog"));
  }

  public override async Task HandleAsync(InstallLogListRequest req, CancellationToken ct)
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

    var response = new PagedResponse<InstallLogListResponse>(new(), 0, req.Page, req.PageSize);

    if (pagedResults is not null)
    {
      var mapper = new InstallLogMapper();

      var entities = pagedResults.Items.Select(i => mapper.FromInstallLogToInstallListLogResponse(i)).ToList();

      response = new PagedResponse<InstallLogListResponse>(entities, pagedResults.Total ?? 0, pagedResults.PageNumber ?? 1, pagedResults.Size);
    }

    await SendAsync(response, cancellation: ct);
    return;
  }
}