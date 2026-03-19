/*
order: 70
title: Reranking
slug: reranking
*/

namespace VoyageAI.IntegrationTests;

public partial class Tests
{
    [TestMethod]
    public async Task Reranking()
    {
        using var client = GetAuthorizedApi();

        var response = await client.RerankerApiAsync(
            query: "What is the capital of France?",
            documents: [
                "Paris is the capital of France.",
                "Berlin is the capital of Germany.",
                "London is the capital of the United Kingdom.",
            ],
            model: "rerank-2-lite",
            topK: 2,
            returnDocuments: true);

        response.Data.Should().NotBeEmpty();
        response.Data!.Count.Should().Be(2);
        response.Data[0].RelevanceScore.Should().NotBeNull();
        response.Data[1].RelevanceScore.Should().NotBeNull();
        response.Data[0].RelevanceScore!.Value.Should().BeGreaterThan(response.Data[1].RelevanceScore!.Value);
    }
}
