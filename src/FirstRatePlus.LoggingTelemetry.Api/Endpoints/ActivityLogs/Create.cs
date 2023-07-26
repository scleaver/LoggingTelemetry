/*
 * Create.cs
 * 
 * This class represents an API endpoint for creating activity logs.
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
/// Create an activity log.
/// </summary>
/// <remarks>Endpoint for creating a batch of activity logs in one request.</remarks>
public class Create : Endpoint<CreateActivityLogRequest, CreateActivityLogResponse>
{
  private readonly IRepository<ActivityLog> _repository;

  public Create(IRepository<ActivityLog> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post(CreateActivityLogRequest.Route);
    AllowAnonymous();
  }

  /// <summary>
  /// Handles the creation of an activity log asynchronously.
  /// </summary>
  /// <param name="req">The activity log data for creation.</param>
  /// <param name="ct">The cancellation token.</param>
  /// <returns>The ID of the created activity log.</returns>
  public override async Task HandleAsync(CreateActivityLogRequest req, CancellationToken ct)
  {
    var mapper = new ActivityLogMapper();

    var newItem = mapper.FromCreateActivityLogRequest(req);

    var createdItem = await _repository.CreateAsync(newItem, ct);

    if (createdItem is null)
    {
      ThrowError("Item not created.");
    }

    var response = new CreateActivityLogResponse
    {
      Id = createdItem.Id
    };

    await SendAsync(response, cancellation: ct);
  }
}
