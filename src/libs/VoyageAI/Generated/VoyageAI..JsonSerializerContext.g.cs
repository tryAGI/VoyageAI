
#nullable enable

#pragma warning disable CS0618 // Type or member is obsolete
#pragma warning disable CS3016 // Arrays as attribute arguments is not CLS-compliant

namespace VoyageAI
{
    /// <summary>
    /// 
    /// </summary>
    [global::System.Text.Json.Serialization.JsonSourceGenerationOptions(
        DefaultIgnoreCondition = global::System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
        Converters = new global::System.Type[] 
        { 
            typeof(global::VoyageAI.JsonConverters.EmbeddingsApiRequestInputTypeJsonConverter),
            typeof(global::VoyageAI.JsonConverters.EmbeddingsApiRequestInputTypeNullableJsonConverter),
            typeof(global::VoyageAI.JsonConverters.EmbeddingsApiRequestOutputDtypeJsonConverter),
            typeof(global::VoyageAI.JsonConverters.EmbeddingsApiRequestOutputDtypeNullableJsonConverter),
            typeof(global::VoyageAI.JsonConverters.EmbeddingsApiRequestEncodingFormatJsonConverter),
            typeof(global::VoyageAI.JsonConverters.EmbeddingsApiRequestEncodingFormatNullableJsonConverter),
            typeof(global::VoyageAI.JsonConverters.MultimodalEmbeddingsApiRequestInputTypeJsonConverter),
            typeof(global::VoyageAI.JsonConverters.MultimodalEmbeddingsApiRequestInputTypeNullableJsonConverter),
            typeof(global::VoyageAI.JsonConverters.MultimodalEmbeddingsApiRequestOutputEncodingJsonConverter),
            typeof(global::VoyageAI.JsonConverters.MultimodalEmbeddingsApiRequestOutputEncodingNullableJsonConverter),
            typeof(global::VoyageAI.JsonConverters.OneOfJsonConverter<string, global::System.Collections.Generic.IList<string>>),
            typeof(global::VoyageAI.JsonConverters.UnixTimestampJsonConverter),
        })]

    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.JsonSerializerContextTypes))]
    public sealed partial class SourceGenerationContext : global::System.Text.Json.Serialization.JsonSerializerContext
    {
    }
}