using System.ComponentModel.DataAnnotations;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ProjectEndpoints;

public class CreateProjectRequest
{
  public const string Route = "/Projects";

  [Required]
  public string? Name { get; set; }
}
