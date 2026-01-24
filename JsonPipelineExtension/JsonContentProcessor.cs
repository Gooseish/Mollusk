using System.ComponentModel;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.Xna.Framework.Content.Pipeline;

using TInput = System.String;
using TOutput = JsonPipelineExtension.JsonContentProcessorResult;

namespace JsonPipelineExtension;

[ContentProcessor(DisplayName = "Json Content Processor")]
internal class JsonContentProcessor : ContentProcessor<TInput, TOutput>
{
    [DisplayName("Minify Json")]
    public bool Minify {get;set;} = true;

    [DisplayName("Runtime Identifier")]
    public string RuntimeIdentifier {get;set;} = string.Empty;
    public override TOutput Process(TInput input, ContentProcessorContext context)
    {
        if (string.IsNullOrEmpty(RuntimeIdentifier))
            throw new System.Exception("No runtime identifier was specified for this json content");
        if (Minify)
            input = MinifyJson(input);
        TOutput result = new()
            { Json = input, RuntimeIdentifier = RuntimeIdentifier };
        return result;
    }
    private string MinifyJson(string input)
    {
        JsonWriterOptions options = new()
        {
            Indented = false
        };

        JsonDocument doc = JsonDocument.Parse(input);

        using (MemoryStream stream = new MemoryStream())
        {
            using (Utf8JsonWriter writer = new Utf8JsonWriter(stream))
            {
                doc.WriteTo(writer);
                writer.Flush();
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }
}

internal class JsonContentProcessorResult
{
    public string Json {get;set;}
    public string RuntimeIdentifier {get;set;}
}

