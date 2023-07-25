using FirstRatePlus.LoggingTelemetry.Api.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace FirstRatePlus.LoggingTelemetry.UnitTests.Api.Converters;

public class JObjectConverterTests
{
  private readonly JObjectConverter _converter;

  public JObjectConverterTests()
  {
    _converter = new JObjectConverter();
  }

  [Fact]
  public void ConvertToCamelCase_Success()
  {
    // Arrange
    var inputJson = "{\"UserName\": \"JohnDoe\", \"Age\": 30}";
    var expectedJson = "{\"userName\": \"JohnDoe\", \"age\": 30}";

    using var reader = new JsonTextReader(new System.IO.StringReader(inputJson));
    reader.Read(); // Advance to the first token

    var serializer = JsonSerializer.CreateDefault(); // Create a default JsonSerializer

    // Act
    var result = _converter.ReadJson(reader, typeof(JObject), null, false, serializer); // Pass the serializer instance

    // Assert
    Assert.True(JToken.DeepEquals(JObject.Parse(expectedJson), result));
  }

  [Fact]
  public void NullInput_ReturnsNull()
  {
    // Arrange
    using var reader = new JsonTextReader(new System.IO.StringReader("null"));
    reader.Read(); // Advance to the first token

    var serializer = JsonSerializer.CreateDefault(); // Create a default JsonSerializer

    // Act
    var result = _converter.ReadJson(reader, typeof(JObject), null, false, serializer); // Pass the serializer instance

    // Assert
    Assert.Null(result);
  }

  [Fact]
  public void EmptyStringInput_ReturnsNull()
  {
    // Arrange
    using var reader = new JsonTextReader(new System.IO.StringReader(""));
    reader.Read(); // Advance to the first token

    var serializer = JsonSerializer.CreateDefault(); // Create a default JsonSerializer

    // Act
    var result = _converter.ReadJson(reader, typeof(JObject), null, false, serializer); // Pass the serializer instance

    // Assert
    Assert.Null(result);
  }
}
