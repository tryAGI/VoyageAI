namespace VoyageAI;

public sealed partial class VoyageAIClient
{
    private string? _apiKey;

    partial void Authorizing(
        global::System.Net.Http.HttpClient client,
        ref string apiKey)
    {
        _apiKey = apiKey;
    }

    partial void Authorized(
        global::System.Net.Http.HttpClient client)
    {
        // The upstream spec defines auth as apiKey with name "Authorization: Bearer",
        // which is invalid as a header name. Override with proper HTTP Bearer auth.
        if (_apiKey != null)
        {
            AuthorizeUsingBearer(_apiKey);
        }
    }
}
