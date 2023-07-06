using Ardalis.HttpClientTestExtensions;
using FirstRatePlus.LoggingTelemetry.Api;
using FirstRatePlus.LoggingTelemetry.Api.Endpoints.ProjectEndpoints;
using Xunit;

namespace FirstRatePlus.LoggingTelemetry.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class ProjectList : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client;

  public ProjectList(CustomWebApplicationFactory<Program> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnsOneProject()
  {
    var result = await _client.GetAndDeserializeAsync<ProjectListResponse>("/Projects");

    Assert.Single(result.Projects);
    Assert.Contains(result.Projects, i => i.Name == SeedData.TestProject1.Name);
  }
}
