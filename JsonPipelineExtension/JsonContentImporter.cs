using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Content.Pipeline;

using TImport = System.String;

namespace JsonPipelineExtension;

[ContentImporter(".json", DisplayName = "Json Importer", DefaultProcessor = nameof(JsonContentProcessor))]
public class JsonContentImporter : ContentImporter<TImport>
{
    public override TImport Import(string filename, ContentImporterContext context)
    {
        TImport json = File.ReadAllText(filename);
        ThrowIfInvalidJson(json);
        return json;
    }
    private void ThrowIfInvalidJson(string json)
    {
        if (string.IsNullOrEmpty(json))
            throw new InvalidContentException("The JSON file is empty.");
        try {
            _ = JsonDocument.Parse(json);}
        catch(Exception ex){
            throw new InvalidContentException("JSON file failed parsing. See inner exception", ex); }
    }

}