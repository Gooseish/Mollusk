using System;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;

namespace JsonPipeline;

public class JsonContentTypeReader<T> : ContentTypeReader<T>
{
    private readonly JsonSerializerOptions options = new()
    {
        Converters = { new Array2DConverter() },
    };
    protected override T Read(ContentReader input, T existingInstance)
    {
        string json = input.ReadString();
        
        T result = JsonSerializer.Deserialize<T>(json, options);
        return result;
    }
}
