# Embedding Generator Get Service Returns Metadata



This example assumes `using VoyageAI;` is in scope and `apiKey` contains your VoyageAI API key.

```csharp
using var client = new VoyageAIClient("test-api-key");
IEmbeddingGenerator<string, Embedding<float>> generator = client;

var metadata = generator.GetService<EmbeddingGeneratorMetadata>();
```