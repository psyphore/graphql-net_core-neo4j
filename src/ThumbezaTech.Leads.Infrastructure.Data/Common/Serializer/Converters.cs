
using System.Reflection;

using Neo4j.Driver;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ThumbezaTech.Leads.Infrastructure.Data.Common.Serializer;

public static class ParameterSerializer
{
  private static readonly JsonSerializerSettings settings = new()
  {
    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
    NullValueHandling = NullValueHandling.Ignore,
    DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate,
    PreserveReferencesHandling = PreserveReferencesHandling.All,
    MissingMemberHandling = MissingMemberHandling.Ignore
  };

  private static readonly JsonSerializerSettings deserializerSettings = new()
  {
    ContractResolver = new PrivateResolver(),
    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
    NullValueHandling = NullValueHandling.Include,
    DefaultValueHandling = DefaultValueHandling.Include,
    PreserveReferencesHandling = PreserveReferencesHandling.All,
  };

  internal sealed class PrivateResolver : DefaultContractResolver
  {
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
      var prop = base.CreateProperty(member, memberSerialization);
      if (!prop.Writable)
      {
        var property = member as PropertyInfo;
        var hasPrivateSetter = property?.GetSetMethod(true) != null;
        prop.Writable = hasPrivateSetter;
      }
      return prop;
    }
  }

  public static string Serialize(this object value)
    => JsonConvert.SerializeObject(value, settings);

  public static T? ProcessRecords<T>(this IRecord record, string label)
    => JsonConvert.DeserializeObject<T>(record[label].Serialize(), deserializerSettings);

  public static T ProcessRecord<T>(this IRecord record, string label)
    => record[label].As<T>();
}

/// <summary>
/// This Converter is only a slightly modified converter from the JSON Extension library.
///
/// All Credit goes to Oskar Gewalli (https://github.com/wallymathieu) and the Makrill Project (https://github.com/NewtonsoftJsonExt/makrill).
/// </summary>
public sealed class CustomDictionaryConverter : JsonConverter
{
  public override bool CanConvert(Type objectType) => objectType == typeof(Dictionary<string, object>);

  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) => ExpectObject(reader);

  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => serializer.Serialize(writer, value);

  private static object ExpectArray(JsonReader reader)
  {
    var array = new List<object>();
    while (reader.Read())
    {
      switch (reader.TokenType)
      {
        case JsonToken.String:
        case JsonToken.Integer:
        case JsonToken.Boolean:
        case JsonToken.Bytes:
        case JsonToken.Date:
        case JsonToken.Float:
        case JsonToken.Null:
          array.Add(reader.Value);
          break;

        case JsonToken.Comment:
          break;

        case JsonToken.StartObject:
          array.Add(ExpectObject(reader));
          break;

        case JsonToken.StartArray:
          array.Add(ExpectArray(reader));
          break;

        case JsonToken.EndArray:
          return array.ToArray();

        default:
          throw new JsonSerializationException($"Unrecognized token: {reader.TokenType}");
      }
    }
    throw new JsonSerializationException("Missing End Token");
  }

  private static object ExpectDictionaryOrArrayOrPrimitive(JsonReader reader)
  {
    reader.Read();
    var startToken = reader.TokenType;
    switch (startToken)
    {
      case JsonToken.String:
      case JsonToken.Integer:
      case JsonToken.Boolean:
      case JsonToken.Bytes:
      case JsonToken.Date:
      case JsonToken.Float:
      case JsonToken.Null:
        return reader.Value;

      case JsonToken.StartObject:
        return ExpectObject(reader);

      case JsonToken.StartArray:
        return ExpectArray(reader);
    }
    throw new JsonSerializationException($"Unrecognized token: {reader.TokenType}");
  }

  private static object ExpectObject(JsonReader reader)
  {
    var dic = new Dictionary<string, object>();

    while (reader.Read())
    {
      switch (reader.TokenType)
      {
        case JsonToken.Comment:
          break;

        case JsonToken.PropertyName:
          dic.Add(reader.Value.ToString(), ExpectDictionaryOrArrayOrPrimitive(reader));
          break;

        case JsonToken.EndObject:
          return dic;

        default:
          throw new JsonSerializationException($"Unrecognized token: {reader.TokenType}");
      }
    }
    throw new JsonSerializationException("Missing End Token");
  }
}

public sealed class NodePaging
{
  public NodePaging(int first, int offset)
  {
    First = first;
    Offset = offset;
  }

  [JsonProperty("first")]
  public int First { get; set; }

  [JsonProperty("offset")]
  public int Offset { get; set; }
}

public static class TypeExtensions
{
  public static bool HasSetter(this PropertyInfo property)
  {
    //In this way we can check for private setters in base classes
    return property.DeclaringType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                 .Any(m => m.Name == "set_" + property.Name);
  }

  public static bool HasGetter(this PropertyInfo property)
  {
    //In this way we can check for private getters in base classes
    return property.DeclaringType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                                 .Any(m => m.Name == "get_" + property.Name);
  }
}
