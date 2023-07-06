﻿using Microsoft.AspNetCore.Mvc;

namespace FirstRatePlus.LoggingTelemetry.Api.Endpoints.ProjectEndpoints;

public class ListIncompleteRequest
{
  [FromRoute]
  public int ProjectId { get; set; }
  [FromQuery]
  public string? SearchString { get; set; }
}
