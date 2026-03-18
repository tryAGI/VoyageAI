namespace VoyageAI.IntegrationTests;

[TestClass]
public partial class Tests
{
    private static VoyageAIClient GetAuthorizedApi()
    {
        var apiKey =
            Environment.GetEnvironmentVariable("VOYAGEAI_API_KEY") ??
            throw new AssertInconclusiveException("VOYAGEAI_API_KEY environment variable is not found.");

        var client = new VoyageAIClient(apiKey);

        return client;
    }
}
