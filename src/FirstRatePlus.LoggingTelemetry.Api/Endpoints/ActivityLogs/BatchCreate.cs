/*
 * BatchCreate.cs
 * 
 * This class represents an API endpoint for creating activity logs as a batch.
 * It inherits from the Endpoint class provided by the FastEndpoints library.
 * The endpoint handles HTTP POST requests to the specified route and allows anonymous access.
 * It uses the Mapperly library for object mapping and the Microsoft.Azure.CosmosRepository library for data access.
 */

using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

/// <summary>
/// Create a batch of activity logs.
/// </summary>
/// <remarks>Endpoint for creating a batch of activity logs in one request.</remarks>
public class BatchCreate : Endpoint<List<CreateActivityLogRequest>, List<CreateActivityLogResponse>>
{
  private readonly IRepository<ActivityLog> _repository;

  public BatchCreate(IRepository<ActivityLog> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post(CreateActivityLogRequest.Route + "/batch");
    AllowAnonymous();
  }

  /// <summary>
  /// Handles the batch creation of activity logs asynchronously.
  /// </summary>
  /// <param name="req">A list of activity log data for creation.</param>
  /// <param name="ct">The cancellation token.</param>
  /// <returns>A list of IDs of the created activity logs.</returns>
  public override async Task HandleAsync(List<CreateActivityLogRequest> req, CancellationToken ct)
  {
    var mapper = new ActivityLogMapper();

    var newItems = req.Select(l => mapper.FromCreateActivityLogRequest(l));

    var items = await _repository.CreateAsync(newItems, ct);

    var response = items.Select(i => new CreateActivityLogResponse { Id = i.Id }).ToList();

    await SendAsync(response, cancellation: ct);
  }
}

