#nullable enable

namespace VoyageAI.JsonConverters
{
    /// <inheritdoc />
    public sealed class MultimodalEmbeddingsApiRequestInputTypeNullableJsonConverter : global::System.Text.Json.Serialization.JsonConverter<global::VoyageAI.MultimodalEmbeddingsApiRequestInputType?>
    {
        /// <inheritdoc />
        public override global::VoyageAI.MultimodalEmbeddingsApiRequestInputType? Read(
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
                        return global::VoyageAI.MultimodalEmbeddingsApiRequestInputTypeExtensions.ToEnum(stringValue);
                    }
                    
                    break;
                }
                case global::System.Text.Json.JsonTokenType.Number:
                {
                    var numValue = reader.GetInt32();
                    return (global::VoyageAI.MultimodalEmbeddingsApiRequestInputType)numValue;
                }
                case global::System.Text.Json.JsonTokenType.Null:
                {
                    return default(global::VoyageAI.MultimodalEmbeddingsApiRequestInputType?);
                }
                default:
                    throw new global::System.ArgumentOutOfRangeException(nameof(reader));
            }

            return default;
        }

        /// <inheritdoc />
        public override void Write(
            global::System.Text.Json.Utf8JsonWriter writer,
            global::VoyageAI.MultimodalEmbeddingsApiRequestInputType? value,
            global::System.Text.Json.JsonSerializerOptions options)
        {
            writer = writer ?? throw new global::System.ArgumentNullException(nameof(writer));

            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(global::VoyageAI.MultimodalEmbeddingsApiRequestInputTypeExtensions.ToValueString(value.Value));
            }
        }
    }
}
