/*
 * Create.cs
 * 
 * This class represents an API endpoint for creating install logs.
 * It inherits from the Endpoint class provided by the FastEndpoints library.
 * The endpoint handles HTTP POST requests to the specified route and allows anonymous access.
 * It uses the Mapperly library for object mapping and the Microsoft.Azure.CosmosRepository library for data access.
 */

using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.MappingProfiles;
using FirstRatePlus.LoggingTelemetry.Core.Aggregates;
using Microsoft.Azure.CosmosRepository;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.InstallLogs;


/// <summary>
/// An API endpoint for creating an activity log.
/// </summary>
public class Create : Endpoint<CreateInstallLogRequest, CreateInstallLogResponse>
{
  private readonly IRepository<InstallLog> _repository;

  public Create(IRepository<InstallLog> repository)
  {
    _repository = repository;
  }

  /// <summary>
  /// Configures the endpoint.
  /// </summary>
  public override void Configure()
  {
    Post(CreateInstallLogRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("InstallLog"));
    Summary(s =>
    {
      s.Summary = "short summary goes here";
      s.Description = "long description goes here";
      s.Responses[200] = "ok response description goes here";
      s.Responses[403] = "forbidden response description goes here";
    });
  }

  /// <summary>
  /// Handles the incoming request to create an install log.
  /// </summary>
  /// <param name="req">The request object containing the install log data.</param>
  /// <param name="ct">Cancellation token.</param>
  public override async Task HandleAsync(
    CreateInstallLogRequest req,
    CancellationToken ct)
  {
    InstallLogMapper mapper = new InstallLogMapper();

    InstallLog newItem = mapper.FromCreateInstallLogRequest(req);

    InstallLog? createdItem = await _repository.CreateAsync(newItem, ct);

    if (createdItem is null)
    {
      ThrowError("Item not created.");
    }

    var response = new CreateInstallLogResponse
    {
      Id = createdItem.Id
    };

    await SendAsync(response, cancellation: ct);
  }
}

