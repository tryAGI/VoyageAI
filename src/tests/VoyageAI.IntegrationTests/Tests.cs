namespace VoyageAI.IntegrationTests;

[TestClass]
public partial class Tests
{
    private static VoyageAIClient GetAuthorizedApi()
    {
        var apiKey =
            Environment.GetEnvironmentVariable("API_KEY") is { Length: > 0 } apiKeyValue ? apiKeyValue :
            Environment.GetEnvironmentVariable("VOYAGEAI_API_KEY") is { Length: > 0 } voyageKeyValue ? voyageKeyValue :
            throw new AssertInconclusiveException("VOYAGEAI_API_KEY environment variable is not found.");

        var httpClient = new HttpClient(new RateLimitRetryHandler());

        var client = new VoyageAIClient(apiKey, httpClient: httpClient);

        return client;
    }
}
