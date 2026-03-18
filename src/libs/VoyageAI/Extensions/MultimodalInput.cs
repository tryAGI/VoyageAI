using System.Text;
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
#pragma warning disable CA1054 // URI parameters should not be strings
    public static MultimodalContent ImageUrl(string url) => new ImageUrlContent(url);
#pragma warning restore CA1054

    /// <summary>
    /// Creates an image content piece from base64-encoded data.
    /// </summary>
    /// <param name="base64DataUrl">Data URL in format: data:[mediatype];base64,[data]</param>
#pragma warning disable CA1054 // URI parameters should not be strings
    public static MultimodalContent ImageBase64(string base64DataUrl) => new ImageBase64Content(base64DataUrl);
#pragma warning restore CA1054

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
        ArgumentNullException.ThrowIfNull(content);

        foreach (var c in content)
        {
            Content.Add(c);
        }
    }
}

/// <summary>
/// A single embedding result from the multimodal embeddings API.
/// </summary>
public sealed class MultimodalEmbeddingData
{
    /// <summary>The embedding vector as a list of floats.</summary>
    public IList<double> Embedding { get; init; } = [];

    /// <summary>The index of this embedding in the input list.</summary>
    public int Index { get; init; }
}

/// <summary>
/// Token usage information from the multimodal embeddings API.
/// </summary>
public sealed class MultimodalEmbeddingUsage
{
    /// <summary>Total tokens consumed.</summary>
    public int TotalTokens { get; init; }
}

/// <summary>
/// Response from the multimodal embeddings API with properly typed embedding vectors.
/// </summary>
public sealed class MultimodalEmbeddingResponse
{
    /// <summary>The embedding results.</summary>
    public IList<MultimodalEmbeddingData> Data { get; init; } = [];

    /// <summary>Token usage information.</summary>
    public MultimodalEmbeddingUsage? Usage { get; init; }

    /// <summary>The model used.</summary>
    public string? Model { get; init; }
}

/// <summary>
/// Extension methods for multimodal embeddings.
/// </summary>
public static class MultimodalEmbeddingsExtensions
{
    /// <summary>
    /// Creates multimodal embeddings using structured inputs.
    /// Bypasses the generated method to correctly serialize/deserialize the inputs and embedding vectors.
    /// </summary>
    public static async Task<MultimodalEmbeddingResponse> MultimodalEmbeddingsAsync(
        this VoyageAIClient client,
        IList<MultimodalInput> inputs,
        string model = "voyage-multimodal-3",
        MultimodalEmbeddingsApiRequestInputType? inputType = null,
        bool? truncation = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(client);
        ArgumentNullException.ThrowIfNull(inputs);

        using var stream = new MemoryStream();
        using (var writer = new Utf8JsonWriter(stream))
        {
            writer.WriteStartObject();
            writer.WritePropertyName("inputs");
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
            writer.WriteString("model", model);
            if (inputType is not null)
            {
                writer.WriteString("input_type", inputType switch
                {
                    MultimodalEmbeddingsApiRequestInputType.Query => "query",
                    MultimodalEmbeddingsApiRequestInputType.Document => "document",
                    _ => throw new ArgumentOutOfRangeException(nameof(inputType)),
                });
            }
            if (truncation is not null)
            {
                writer.WriteBoolean("truncation", truncation.Value);
            }
            writer.WriteEndObject();
        }

        var json = Encoding.UTF8.GetString(stream.ToArray());

        using var httpRequest = new HttpRequestMessage(HttpMethod.Post,
            new Uri((client.BaseUri ?? new Uri("https://api.voyageai.com/v1")).ToString().TrimEnd('/') + "/multimodalembeddings"));
        foreach (var auth in client.Authorizations)
        {
            if (auth.Type is "Http" or "OAuth2")
            {
                httpRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(
                    auth.Name, auth.Value);
            }
            else if (auth.Type == "ApiKey" && auth.Location == "Header")
            {
                httpRequest.Headers.Add(auth.Name, auth.Value);
            }
        }
        httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");

        using var response = await client.HttpClient.SendAsync(httpRequest, cancellationToken).ConfigureAwait(false);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        using var doc = JsonDocument.Parse(responseContent);
        var root = doc.RootElement;

        var data = new List<MultimodalEmbeddingData>();
        if (root.TryGetProperty("data", out var dataArray))
        {
            foreach (var item in dataArray.EnumerateArray())
            {
                var embedding = new List<double>();
                if (item.TryGetProperty("embedding", out var embeddingArray))
                {
                    foreach (var val in embeddingArray.EnumerateArray())
                    {
                        embedding.Add(val.GetDouble());
                    }
                }

                data.Add(new MultimodalEmbeddingData
                {
                    Embedding = embedding,
                    Index = item.TryGetProperty("index", out var idx) ? idx.GetInt32() : 0,
                });
            }
        }

        MultimodalEmbeddingUsage? usage = null;
        if (root.TryGetProperty("usage", out var usageEl) &&
            usageEl.TryGetProperty("total_tokens", out var totalTokens))
        {
            usage = new MultimodalEmbeddingUsage { TotalTokens = totalTokens.GetInt32() };
        }

        return new MultimodalEmbeddingResponse
        {
            Data = data,
            Usage = usage,
            Model = root.TryGetProperty("model", out var modelProp) ? modelProp.GetString() : null,
        };
    }
}
