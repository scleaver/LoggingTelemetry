using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Core.Entities;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogEndpoints;

public class Create : Endpoint<CreateInstallLogRequest, CreateInstallLogResponse>
{
  private readonly IRepository<InstallLog> _repository;

  public Create(IRepository<InstallLog> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post(CreateInstallLogRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("InstallLog"));
  }

  public override async Task HandleAsync(
    CreateInstallLogRequest req,
    CancellationToken ct)
  {
    if(req.UserId == null || req.MachineId == null)
    {
      ThrowError("UserId and MachineId are required.");
    }

    var newItem = new InstallLog(req.UserId, req.MachineId, req.ReleaseNumber, req.SoftwareName ?? "");

    var createdItem = await _repository.CreateAsync(newItem, ct);

    if(createdItem == null)
    {
      ThrowError("Item not created");
    }
    
    var response = new CreateInstallLogResponse
    {
      InstallId = createdItem.Id,
      UserId = createdItem.UserId,
      MachineId = newItem.MachineId,
      DateCreatedUtc = newItem.DateCreatedUtc,
      ReleaseNumber = newItem.ReleaseNumber,
      SoftwareName = newItem.SoftwareName ?? ""
    };

    await SendAsync(response, cancellation: ct);
  }
}

