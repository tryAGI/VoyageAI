/*
order: 60
title: Multimodal Embeddings Text Only
slug: multimodal-embeddings-text-only
*/

namespace VoyageAI.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task MultimodalEmbeddings_TextOnly()
    {
        using var client = GetAuthorizedApi();

        var inputs = new List<MultimodalInput>
        {
            new(MultimodalContent.Text("Hello, world!")),
        };

        var response = await client.MultimodalEmbeddingsAsync(
            inputs: inputs,
            model: "voyage-multimodal-3");

        response.Data.Should().NotBeEmpty();
        response.Data[0].Embedding.Should().NotBeEmpty();
        response.Usage.Should().NotBeNull();
    }
}
