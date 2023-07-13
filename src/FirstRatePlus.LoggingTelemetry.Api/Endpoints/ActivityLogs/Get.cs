using System.Net;
using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

public class Get : Endpoint<GetActivityLogRequest, GetActivityLogResponse>
{
  private readonly IRepository<ActivityLog> _repository;

  public Get(IRepository<ActivityLog> repository)
  {
    _repository = repository;
  }

  /// <summary>
  /// Configures the endpoint.
  /// </summary>
  public override void Configure()
  {
    Get(GetActivityLogRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ActivityLog"));
  }

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

