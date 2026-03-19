/*
order: 40
title: Embeddings
slug: embeddings
*/

namespace VoyageAI.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Embeddings()
    {
        using var client = GetAuthorizedApi();

        var response = await client.EmbeddingsApiAsync(
            input: "Hello, world!",
            model: "voyage-3-lite");

        response.Data.Should().NotBeEmpty();
    }
}
