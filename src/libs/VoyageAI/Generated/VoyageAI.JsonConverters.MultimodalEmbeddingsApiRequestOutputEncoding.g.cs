#nullable enable

namespace VoyageAI.JsonConverters
{
    /// <inheritdoc />
    public sealed class MultimodalEmbeddingsApiRequestOutputEncodingJsonConverter : global::System.Text.Json.Serialization.JsonConverter<global::VoyageAI.MultimodalEmbeddingsApiRequestOutputEncoding>
    {
        /// <inheritdoc />
        public override global::VoyageAI.MultimodalEmbeddingsApiRequestOutputEncoding Read(
            ref global::System.Text.Json.Utf8JsonReader reader,
            global::System.Type typeToConvert,
            global::System.Text.Json.JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case global::System.Text.Json.JsonTokenType.String:
                {
                    var stringValue = reader.GetString();
                    if (stringValue != null)
                    {
                        return global::VoyageAI.MultimodalEmbeddingsApiRequestOutputEncodingExtensions.ToEnum(stringValue) ?? default;
                    }
                    
                    break;
                }
                case global::System.Text.Json.JsonTokenType.Number:
                {
                    var numValue = reader.GetInt32();
                    return (global::VoyageAI.MultimodalEmbeddingsApiRequestOutputEncoding)numValue;
                }
                case global::System.Text.Json.JsonTokenType.Null:
                {
                    return default(global::VoyageAI.MultimodalEmbeddingsApiRequestOutputEncoding);
                }
                default:
                    throw new global::System.ArgumentOutOfRangeException(nameof(reader));
            }

            return default;
        }

        /// <inheritdoc />
        public override void Write(
            global::System.Text.Json.Utf8JsonWriter writer,
            global::VoyageAI.MultimodalEmbeddingsApiRequestOutputEncoding value,
            global::System.Text.Json.JsonSerializerOptions options)
        {
            writer = writer ?? throw new global::System.ArgumentNullException(nameof(writer));

            writer.WriteStringValue(global::VoyageAI.MultimodalEmbeddingsApiRequestOutputEncodingExtensions.ToValueString(value));
        }
    }
}
