/*
order: 50
title: Multimodal Embeddings Text And Image Url
slug: multimodal-embeddings-text-and-image-url
*/

namespace VoyageAI.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task MultimodalEmbeddings_TextAndImageUrl()
    {
        using var client = GetAuthorizedApi();

        var inputs = new List<MultimodalInput>
        {
            new(
                MultimodalContent.Text("This is a banana."),
                MultimodalContent.ImageUrl("https://raw.githubusercontent.com/voyage-ai/voyage-multimodal-3/refs/heads/main/images/banana.jpg")),
        };

        var response = await client.MultimodalEmbeddingsAsync(
            inputs: inputs,
            model: "voyage-multimodal-3");

        response.Data.Should().NotBeEmpty();
        response.Data[0].Embedding.Should().NotBeEmpty();
    }
}
