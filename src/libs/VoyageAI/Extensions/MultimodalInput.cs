using System.Text.Json;

namespace VoyageAI;

/// <summary>
/// Represents a single content piece in a multimodal embedding input.
/// </summary>
public abstract class MultimodalContent
{
    /// <summary>
    /// Creates a text content piece.
    /// </summary>
    public static MultimodalContent Text(string text) => new TextContent(text);

    /// <summary>
    /// Creates an image content piece from a URL.
    /// </summary>
    public static MultimodalContent ImageUrl(string url) => new ImageUrlContent(url);

    /// <summary>
    /// Creates an image content piece from base64-encoded data.
    /// </summary>
    /// <param name="base64DataUrl">Data URL in format: data:[mediatype];base64,[data]</param>
    public static MultimodalContent ImageBase64(string base64DataUrl) => new ImageBase64Content(base64DataUrl);

    internal abstract void WriteTo(Utf8JsonWriter writer);
}

internal sealed class TextContent(string text) : MultimodalContent
{
    internal override void WriteTo(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("type", "text");
        writer.WriteString("text", text);
        writer.WriteEndObject();
    }
}

internal sealed class ImageUrlContent(string url) : MultimodalContent
{
    internal override void WriteTo(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("type", "image_url");
        writer.WriteString("image_url", url);
        writer.WriteEndObject();
    }
}

internal sealed class ImageBase64Content(string base64DataUrl) : MultimodalContent
{
    internal override void WriteTo(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("type", "image_base64");
        writer.WriteString("image_base64", base64DataUrl);
        writer.WriteEndObject();
    }
}

/// <summary>
/// Represents a single multimodal input containing a sequence of text and/or images.
/// </summary>
public sealed class MultimodalInput
{
    /// <summary>
    /// The content pieces (text and/or images) for this input.
    /// </summary>
    public IList<MultimodalContent> Content { get; } = new List<MultimodalContent>();

    /// <summary>
    /// Creates a multimodal input with the given content pieces.
    /// </summary>
    public MultimodalInput(params MultimodalContent[] content)
    {
        foreach (var c in content)
        {
            Content.Add(c);
        }
    }
}

/// <summary>
/// Extension methods for multimodal embeddings.
/// </summary>
public static class MultimodalEmbeddingsExtensions
{
    /// <summary>
    /// Serializes multimodal inputs to the byte[] format expected by the generated API.
    /// </summary>
    public static byte[] ToInputBytes(this IList<MultimodalInput> inputs)
    {
        using var stream = new MemoryStream();
        using var writer = new Utf8JsonWriter(stream);

        writer.WriteStartArray();
        foreach (var input in inputs)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("content");
            writer.WriteStartArray();
            foreach (var content in input.Content)
            {
                content.WriteTo(writer);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        writer.WriteEndArray();
        writer.Flush();

        return stream.ToArray();
    }

    /// <summary>
    /// Creates multimodal embeddings using structured inputs.
    /// </summary>
    /// <param name="client">The VoyageAI client.</param>
    /// <param name="inputs">List of multimodal inputs (text + images).</param>
    /// <param name="model">Model name (e.g., "voyage-multimodal-3").</param>
    /// <param name="inputType">Optional input type (query or document).</param>
    /// <param name="truncation">Whether to truncate over-length inputs. Defaults to true.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    public static Task<MultimodalEmbeddingsApiResponse> MultimodalEmbeddingsAsync(
        this VoyageAIClient client,
        IList<MultimodalInput> inputs,
        string model = "voyage-multimodal-3",
        MultimodalEmbeddingsApiRequestInputType? inputType = null,
        bool? truncation = null,
        CancellationToken cancellationToken = default)
    {
        return client.MultimodalEmbeddingsApiAsync(
            inputs: inputs.ToInputBytes(),
            model: model,
            inputType: inputType,
            truncation: truncation,
            cancellationToken: cancellationToken);
    }
}
