using Meai = Microsoft.Extensions.AI;

namespace VoyageAI;

public partial class VoyageAIClient : Meai.IEmbeddingGenerator<string, Meai.Embedding<float>>
{
    private Meai.EmbeddingGeneratorMetadata? _embeddingMetadata;

    object? Meai.IEmbeddingGenerator.GetService(Type serviceType, object? serviceKey)
    {
        ArgumentNullException.ThrowIfNull(serviceType);

        return
            serviceKey is not null ? null :
            serviceType == typeof(Meai.EmbeddingGeneratorMetadata)
                ? (_embeddingMetadata ??= new(nameof(VoyageAIClient), BaseUri))
                : serviceType.IsInstanceOfType(this) ? this
                : null;
    }

    async Task<Meai.GeneratedEmbeddings<Meai.Embedding<float>>>
        Meai.IEmbeddingGenerator<string, Meai.Embedding<float>>.GenerateAsync(
            IEnumerable<string> values,
            Meai.EmbeddingGenerationOptions? options,
            CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(values);

        var textList = values.ToList();

        var request = new EmbeddingsApiRequest
        {
            Model = options?.ModelId ?? "voyage-3-lite",
            Input = textList.Count == 1
                ? new OneOf<string, global::System.Collections.Generic.IList<string>>(textList[0])
                : new OneOf<string, global::System.Collections.Generic.IList<string>>(textList),
        };

        if (options?.Dimensions is { } dimensions)
        {
            request.OutputDimension = dimensions;
        }

        var response = await EmbeddingsApiAsync(request, cancellationToken).ConfigureAwait(false);

        var embeddings = new Meai.GeneratedEmbeddings<Meai.Embedding<float>>();

        if (response.Data is { } data)
        {
            foreach (var item in data)
            {
                if (item.Embedding is { } embeddingList)
                {
                    var floatArray = new float[embeddingList.Count];
                    for (var i = 0; i < embeddingList.Count; i++)
                    {
                        floatArray[i] = (float)embeddingList[i];
                    }

                    embeddings.Add(new Meai.Embedding<float>(floatArray)
                    {
                        ModelId = response.Model,
                    });
                }
            }
        }

        if (response.Usage?.TotalTokens is { } totalTokens)
        {
            embeddings.Usage = new Meai.UsageDetails
            {
                InputTokenCount = totalTokens,
                TotalTokenCount = totalTokens,
            };
        }

        return embeddings;
    }
}
