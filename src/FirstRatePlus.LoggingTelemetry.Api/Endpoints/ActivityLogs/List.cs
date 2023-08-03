/*
 * List.cs
 * 
 * This class represents an API endpoint for getting a collection of activity logs.
 * It inherits from the Endpoint class provided by the FastEndpoints library.
 * The endpoint handles HTTP GET requests to the specified route and allows anonymous access.
 * It uses the Mapperly library for object mapping and the Microsoft.Azure.CosmosRepository library for data access.
 */

using System.Linq.Expressions;
using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using FirstRatePlus.SharedApplication.ApiModels;
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

    Expression<Func<ActivityLog, bool>> filterExpression;

    if (req.UserId is not null && req.UserId != Guid.Empty)
    {
      filterExpression = i => i.ActivityDateUtc >= req.DateFrom &&
                              i.ActivityDateUtc <= req.DateTo &&
                              i.UserId == req.UserId.ToString();
    }
    else
    {
      filterExpression = i => i.ActivityDateUtc >= req.DateFrom &&
                              i.ActivityDateUtc <= req.DateTo;
    }

    var pagedResults = await _repository.PageAsync(filterExpression, req.Page, req.PageSize, true, ct);

    if (req.UserId is not null && req.UserId != Guid.Empty)
    {
      // Add a filter for the user id.
    }

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

