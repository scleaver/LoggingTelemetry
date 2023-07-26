/*
 * Get.cs
 * 
 * This class represents an API endpoint for getting an activity log by the specified ID.
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

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

/// <summary>
/// Get an activity log.
/// </summary>
/// <remarks>Get an activity log by the specified ID.</remarks>
public class Get : Endpoint<GetActivityLogRequest, GetActivityLogResponse>
{
  private readonly IRepository<ActivityLog> _repository;

  public Get(IRepository<ActivityLog> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Get(GetActivityLogRequest.Route);
    AllowAnonymous();
  }

  /// <summary>
  /// Handles the fecthing of activity logs.
  /// </summary>
  /// <param name="req"></param>
  /// <param name="ct"></param>
  /// <returns></returns>
  public override async Task HandleAsync(GetActivityLogRequest req, CancellationToken ct)
  {
    try
    {
      ActivityLog entity = await _repository.GetAsync(req.Id, null, ct);

      ActivityLogMapper mapper = new ActivityLogMapper();

      var response = mapper.ToGetActivityLogResponse(entity);

      await SendAsync(response, cancellation: ct);
    }
    catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
    {
      await SendNotFoundAsync(ct);
      return;
    }
  }
}

