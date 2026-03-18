#nullable enable

namespace VoyageAI.JsonConverters
{
    /// <inheritdoc />
    public sealed class EmbeddingsApiRequestInputTypeNullableJsonConverter : global::System.Text.Json.Serialization.JsonConverter<global::VoyageAI.EmbeddingsApiRequestInputType?>
    {
        /// <inheritdoc />
        public override global::VoyageAI.EmbeddingsApiRequestInputType? Read(
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
                        return global::VoyageAI.EmbeddingsApiRequestInputTypeExtensions.ToEnum(stringValue);
                    }
                    
                    break;
                }
                case global::System.Text.Json.JsonTokenType.Number:
                {
                    var numValue = reader.GetInt32();
                    return (global::VoyageAI.EmbeddingsApiRequestInputType)numValue;
                }
                case global::System.Text.Json.JsonTokenType.Null:
                {
                    return default(global::VoyageAI.EmbeddingsApiRequestInputType?);
                }
                default:
                    throw new global::System.ArgumentOutOfRangeException(nameof(reader));
            }

            return default;
        }

        /// <inheritdoc />
        public override void Write(
            global::System.Text.Json.Utf8JsonWriter writer,
            global::VoyageAI.EmbeddingsApiRequestInputType? value,
            global::System.Text.Json.JsonSerializerOptions options)
        {
            writer = writer ?? throw new global::System.ArgumentNullException(nameof(writer));

            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStringValue(global::VoyageAI.EmbeddingsApiRequestInputTypeExtensions.ToValueString(value.Value));
            }
        }
    }
}
