
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
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.EmbeddingsApiRequest))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.OneOf<string, global::System.Collections.Generic.IList<string>>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(string))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.IList<string>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.EmbeddingsApiRequestInputType))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(bool))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(int))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.EmbeddingsApiRequestOutputDtype))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.EmbeddingsApiRequestEncodingFormat))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.MultimodalEmbeddingsApiRequest))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(byte[]))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.MultimodalEmbeddingsApiRequestInputType))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.MultimodalEmbeddingsApiRequestOutputEncoding))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.RerankerApiRequest))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.EmbeddingsApiResponse))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.IList<global::VoyageAI.EmbeddingsApiResponseDataItem>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.EmbeddingsApiResponseDataItem))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.IList<double>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(double))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.EmbeddingsApiResponseUsage))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.EmbeddingsApiResponse2))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.MultimodalEmbeddingsApiResponse))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.IList<global::VoyageAI.MultimodalEmbeddingsApiResponseDataItem>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.MultimodalEmbeddingsApiResponseDataItem))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.MultimodalEmbeddingsApiResponseUsage))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.MultimodalEmbeddingsApiResponse2))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.RerankerApiResponse))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.IList<global::VoyageAI.RerankerApiResponseDataItem>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.RerankerApiResponseDataItem))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.RerankerApiResponseUsage))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.RerankerApiResponse2))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::VoyageAI.OneOf<string, global::System.Collections.Generic.List<string>>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.List<string>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.List<global::VoyageAI.EmbeddingsApiResponseDataItem>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.List<double>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.List<global::VoyageAI.MultimodalEmbeddingsApiResponseDataItem>))]
    [global::System.Text.Json.Serialization.JsonSerializable(typeof(global::System.Collections.Generic.List<global::VoyageAI.RerankerApiResponseDataItem>))]
    public sealed partial class SourceGenerationContext : global::System.Text.Json.Serialization.JsonSerializerContext
    {
    }
}