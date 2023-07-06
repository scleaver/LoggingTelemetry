using System.ComponentModel.DataAnnotations;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ContributorEndpoints;

public class CreateContributorRequest
{
  public const string Route = "/Contributors";

  [Required]
  public string? Name { get; set; }
}
