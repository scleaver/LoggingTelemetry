using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;

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
    Options(x => x
      .WithTags("ActivityLog"));
    Summary(s =>
    {
      s.Summary = "short summary goes here";
      s.Description = "long description goes here";
      s.Responses[200] = "ok response description goes here";
      s.Responses[403] = "forbidden response description goes here";
    });
  }

  public override async Task HandleAsync(List<CreateActivityLogRequest> req, CancellationToken ct)
  {
    var mapper = new ActivityLogMapper();

    var newItems = req.Select(l => mapper.RequestToActivityLog(l));

    var items = await _repository.CreateAsync(newItems, ct);

    var response = items.Select(i => new CreateActivityLogResponse { Id = i.Id }).ToList();

    await SendAsync(response, cancellation: ct);
  }
}

