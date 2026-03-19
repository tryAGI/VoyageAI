# Embeddings



This example assumes `using VoyageAI;` is in scope and `apiKey` contains your VoyageAI API key.

```csharp
using var client = GetAuthorizedApi();

var response = await client.EmbeddingsApiAsync(
    input: "Hello, world!",
    model: "voyage-3-lite");
```