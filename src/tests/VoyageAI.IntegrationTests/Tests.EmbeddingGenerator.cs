using Microsoft.Extensions.AI;

namespace VoyageAI.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public void EmbeddingGenerator_GetService_ReturnsMetadata()
    {
        using var client = new VoyageAIClient("test-api-key");
        IEmbeddingGenerator<string, Embedding<float>> generator = client;

        var metadata = generator.GetService<EmbeddingGeneratorMetadata>();

        metadata.Should().NotBeNull();
        metadata!.ProviderName.Should().Be(nameof(VoyageAIClient));
    }

    [TestMethod]
    public void EmbeddingGenerator_GetService_ReturnsSelf()
    {
        using var client = new VoyageAIClient("test-api-key");
        IEmbeddingGenerator<string, Embedding<float>> generator = client;

        var self = generator.GetService<VoyageAIClient>();

        self.Should().BeSameAs(client);
    }

    [TestMethod]
    public async Task EmbeddingGenerator_GenerateAsync()
    {
        using var client = GetAuthorizedApi();
        IEmbeddingGenerator<string, Embedding<float>> generator = client;

        var embeddings = await generator.GenerateAsync(
            ["Hello, world!"],
            new EmbeddingGenerationOptions
            {
                ModelId = "voyage-3-lite",
            });

        embeddings.Should().HaveCount(1);
        embeddings[0].Vector.Length.Should().BeGreaterThan(0);
        embeddings.Usage.Should().NotBeNull();
        embeddings.Usage!.TotalTokenCount.Should().BeGreaterThan(0);
    }
}
