
#nullable enable

namespace VoyageAI
{
    public sealed partial class VoyageAIClient
    {
        /// <inheritdoc/>
        public void AuthorizeUsingApiKeyInHeader(
            string apiKey)
        {
            apiKey = apiKey ?? throw new global::System.ArgumentNullException(nameof(apiKey));

            Authorizations.Clear();
            Authorizations.Add(new global::VoyageAI.EndPointAuthorization
            {
                Type = "ApiKey",
                Location = "Header",
                Name = "Authorization: Bearer",
                Value = apiKey,
            });
        }
    }
}