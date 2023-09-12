using Newtonsoft.Json;

namespace FirstRatePlus.LoggingTelemetry.Api.Middlewares;

/// <summary>
/// Middleware to handle JSON serialization and deserialization exceptions.
/// </summary>
public class JsonExceptionMiddleware
{
  // The next middleware in the pipeline
  private readonly RequestDelegate _next;

  // Logger instance for logging errors and other information
  private readonly ILogger<JsonExceptionMiddleware> _logger;

  /// <summary>
  /// Initializes a new instance of the <see cref="JsonExceptionMiddleware"/> class.
  /// </summary>
  /// <param name="next">The next middleware in the pipeline.</param>
  /// <param name="logger">The logger instance.</param>
  public JsonExceptionMiddleware(RequestDelegate next, ILogger<JsonExceptionMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }

  /// <summary>
  /// Invokes the middleware to handle JSON exceptions.
  /// </summary>
  /// <param name="context">The HTTP context.</param>
  /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
  public async Task InvokeAsync(HttpContext context)
  {
    try
    {
      // Proceed to the next middleware in the pipeline
      await _next(context);
    }
    catch (Exception ex) when (ex is JsonSerializationException || ex is JsonReaderException)
    {
      // Log the exception
      _logger.LogError(ex, "An error occurred while processing the request.");

      // Set the response status code and content type
      context.Response.StatusCode = 400; // Bad Request
      context.Response.ContentType = "application/json";

      // Create the error response object
      var response = new
      {
        statusCode = 400,
        message = "One or more errors occurred!",
        errors = new
        {
          request = new[] { "Invalid JSON format." }
        }
      };

      // Serialize the error response object to JSON
      var jsonResponse = JsonConvert.SerializeObject(response);

      // Write the JSON response to the HTTP response
      await context.Response.WriteAsync(jsonResponse);
    }
  }
}
