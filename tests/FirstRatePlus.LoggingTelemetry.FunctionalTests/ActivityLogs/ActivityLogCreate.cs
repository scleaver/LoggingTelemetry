﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FastEndpoints;
using FirstRatePlus.LoggingTelemetry.Api.Endpoints.ActivityLogs;
using Xunit;
using Ardalis.HttpClientTestExtensions;
using Newtonsoft.Json.Linq;
using Shouldly;
using Ardalis.Result;

namespace FirstRatePlus.LoggingTelemetry.FunctionalTests.ActivityLogs;

[Collection("Sequential")]
public class ActivityLogCreate : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client;

  public ActivityLogCreate(CustomWebApplicationFactory<Program> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task PostActivityLog_ReturnsOneActivityLog()
  {
    var softwareName = "Test " + Guid.NewGuid().ToString();
    var userId = Guid.NewGuid().ToString();
    var data = new { key1 = "value1", key2 = "value2", key3 = "value3" };
    var request = new CreateActivityLogRequest()
    {
      ActivityDate = DateTime.UtcNow.AddDays(-1),
      UserId = userId,
      ReleaseNumber = 55000,
      SoftwareName = softwareName,
      Data = new JObject(data)
    };

    var content = StringContentHelpers.FromModelAsJson(request);

    var response = await _client.PostAndDeserializeAsync<CreateActivityLogResponse>(
      CreateActivityLogRequest.Route, content);

    response.ShouldNotBeNull();
    response.Id.ShouldNotBeNullOrEmpty();
  }

  [Fact]
  public async Task PostActivityLog_InvalidData_ReturnsBadRequest()
  {
    // Arrange
    var invalidData = "{ \"UserId\": \"12345\", \"ReleaseNumber\": 1, \"ActivityDate\": \"2023-09-13T12:34:56Z\", \"SoftwareName\": \"MySoftware\", \"Data\": \"{ invalid_json }\" }";

    // Act
    var response = await _client.PostAsJsonAsync(CreateActivityLogRequest.Route, invalidData);

    // Assert
    response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
  }
}
