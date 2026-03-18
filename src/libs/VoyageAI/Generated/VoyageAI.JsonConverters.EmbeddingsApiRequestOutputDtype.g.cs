#nullable enable

namespace VoyageAI.JsonConverters
{
    /// <inheritdoc />
    public sealed class EmbeddingsApiRequestOutputDtypeJsonConverter : global::System.Text.Json.Serialization.JsonConverter<global::VoyageAI.EmbeddingsApiRequestOutputDtype>
    {
        /// <inheritdoc />
        public override global::VoyageAI.EmbeddingsApiRequestOutputDtype Read(
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
                        return global::VoyageAI.EmbeddingsApiRequestOutputDtypeExtensions.ToEnum(stringValue) ?? default;
                    }
                    
                    break;
                }
                case global::System.Text.Json.JsonTokenType.Number:
                {
                    var numValue = reader.GetInt32();
                    return (global::VoyageAI.EmbeddingsApiRequestOutputDtype)numValue;
                }
                case global::System.Text.Json.JsonTokenType.Null:
                {
                    return default(global::VoyageAI.EmbeddingsApiRequestOutputDtype);
                }
                default:
                    throw new global::System.ArgumentOutOfRangeException(nameof(reader));
            }

            return default;
        }

        /// <inheritdoc />
        public override void Write(
            global::System.Text.Json.Utf8JsonWriter writer,
            global::VoyageAI.EmbeddingsApiRequestOutputDtype value,
            global::System.Text.Json.JsonSerializerOptions options)
        {
            writer = writer ?? throw new global::System.ArgumentNullException(nameof(writer));

            writer.WriteStringValue(global::VoyageAI.EmbeddingsApiRequestOutputDtypeExtensions.ToValueString(value));
        }
    }
}
