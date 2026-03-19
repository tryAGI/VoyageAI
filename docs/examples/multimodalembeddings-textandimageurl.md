# MultimodalEmbeddings.TextAndImageUrl



This example assumes `using VoyageAI;` is in scope and `apiKey` contains your VoyageAI API key.

```csharp
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
```