/*
order: 10
title: Embedding Generator Generate Async
slug: embedding-generator-generate-async
*/

using Microsoft.Extensions.AI;

namespace VoyageAI.IntegrationTests;

public partial class Tests
{
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
