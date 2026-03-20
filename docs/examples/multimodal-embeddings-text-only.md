# Multimodal Embeddings Text Only



This example assumes `using VoyageAI;` is in scope and `apiKey` contains your VoyageAI API key.

```csharp
using var client = GetAuthorizedApi();

var inputs = new List<MultimodalInput>
{
    new(MultimodalContent.Text("Hello, world!")),
};

var response = await client.MultimodalEmbeddingsAsync(
    inputs: inputs,
    model: "voyage-multimodal-3");
```