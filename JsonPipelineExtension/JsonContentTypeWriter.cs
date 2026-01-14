using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;
using TInput = JsonPipelineExtension.JsonContentProcessorResult;

namespace JsonPipelineExtension;

[ContentTypeWriter]
internal class JsonContentTypeWriter : ContentTypeWriter<TInput>
{
    private string _runtimeIdentifier;
    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return _runtimeIdentifier;
    }

    protected override void Write(ContentWriter output, TInput value)
    {
        _runtimeIdentifier = value.RuntimeIdentifier;
        output.Write(value.Json);
    }
}
