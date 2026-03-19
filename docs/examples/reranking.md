# Reranking



This example assumes `using VoyageAI;` is in scope and `apiKey` contains your VoyageAI API key.

```csharp
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
```