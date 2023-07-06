namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ProjectEndpoints;

public class CreateProjectResponse
{
  public CreateProjectResponse(int id, string name)
  {
    Id = id;
    Name = name;
  }
  public int Id { get; set; }
  public string Name { get; set; }
}
