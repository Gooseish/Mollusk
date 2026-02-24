using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

public class ValueTupleConverter<T1, T2> : JsonConverter<(T1, T2)>
{
    public override (T1, T2) Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var jArray = JsonSerializer.Deserialize<JsonArray> (ref reader, options);
        T1 item1 = JsonSerializer.Deserialize<T1>(jArray[0].ToJsonString());
        T2 item2 = JsonSerializer.Deserialize<T2>(jArray[1].ToJsonString());
        return (item1, item2);
    }
    public override void Write(Utf8JsonWriter writer, (T1, T2) value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        JsonSerializer.Serialize(writer, value.Item1, options);
        JsonSerializer.Serialize(writer, value.Item2, options);
        writer.WriteEndArray();
    }
}