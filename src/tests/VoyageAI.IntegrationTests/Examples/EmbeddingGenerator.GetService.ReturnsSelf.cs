/*
order: 30
title: Embedding Generator Get Service Returns Self
slug: embedding-generator-get-service-returns-self
*/

using Microsoft.Extensions.AI;

namespace VoyageAI.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public void EmbeddingGenerator_GetService_ReturnsSelf()
    {
        using var client = new VoyageAIClient("test-api-key");
        IEmbeddingGenerator<string, Embedding<float>> generator = client;

        var self = generator.GetService<VoyageAIClient>();

        self.Should().BeSameAs(client);
    }
}
