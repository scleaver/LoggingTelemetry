using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace FirstRatePlus.LoggingTelemetry.Api.Converters;

/// <summary>
/// Converts a JObject to camelCase during deserialization and serialization.
/// </summary>
public class JObjectConverter : JsonConverter<JObject>
{
  /// <inheritdoc/>
  public override JObject? ReadJson(JsonReader reader, Type objectType, JObject? existingValue, bool hasExistingValue, JsonSerializer serializer)
  {
    if (reader.TokenType == JsonToken.Null || reader.TokenType == JsonToken.None)
    {
      return null;
    }

    if (reader.TokenType == JsonToken.StartObject)
    {
      var jObject = JObject.Load(reader);

      jObject = CamelCaseData(jObject);

      return jObject;
    }

    throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}. Expected JSON object.");
  }

  /// <summary>
  /// Converts the property names of a JObject to camelCase.
  /// </summary>
  /// <param name="jObject">The input JObject to be converted.</param>
  /// <returns>A new JObject with property names converted to camelCase.</returns>
  private JObject? CamelCaseData(JObject jObject)
  {
    // Deserialize the JObject to an ExpandoObject with ExpandoObjectConverter to allow dynamic property names.
    var camelCaseData = JsonConvert.DeserializeObject<ExpandoObject>(jObject?.ToString() ?? "", new ExpandoObjectConverter());

    // Configure JsonSerializer settings to use CamelCasePropertyNamesContractResolver for property name conversion.
    var settings = new JsonSerializer
    {
      ContractResolver = new CamelCasePropertyNamesContractResolver()
    };

    // Serialize the ExpandoObject back to a new JObject with camelCase property names.
    return JObject.FromObject(camelCaseData ?? new ExpandoObject(), settings);
  }

  /// <inheritdoc/>
  public override void WriteJson(JsonWriter writer, JObject? value, JsonSerializer serializer)
  {
    // Write the JObject to the JSON writer.
    value?.WriteTo(writer);
  }
}