# Microsoft.Extensions.AI Integration

!!! tip "Cross-SDK comparison"
    See the [centralized MEAI documentation](https://tryagi.github.io/docs/meai/) for feature matrices and comparisons across all tryAGI SDKs.

The VoyageAI SDK implements `IEmbeddingGenerator<string, Embedding<float>>` from [Microsoft.Extensions.AI](https://learn.microsoft.com/en-us/dotnet/ai/microsoft-extensions-ai).

## Installation

```bash
dotnet add package tryAGI.VoyageAI
```

## Generate Embeddings

`VoyageAIClient` directly implements `IEmbeddingGenerator<string, Embedding<float>>`, so you can use it with the standard MEAI interface.

```csharp
using Microsoft.Extensions.AI;
using VoyageAI;

IEmbeddingGenerator<string, Embedding<float>> generator =
    new VoyageAIClient(apiKey: Environment.GetEnvironmentVariable("VOYAGEAI_API_KEY")!);

var embeddings = await generator.GenerateAsync(
    ["Hello, world!"],
    new EmbeddingGenerationOptions { ModelId = "voyage-3" });

Console.WriteLine($"Dimensions: {embeddings[0].Vector.Length}");
Console.WriteLine($"Model: {embeddings[0].ModelId}");
```

## Batch Embeddings

```csharp
using Microsoft.Extensions.AI;
using VoyageAI;

IEmbeddingGenerator<string, Embedding<float>> generator =
    new VoyageAIClient(apiKey: Environment.GetEnvironmentVariable("VOYAGEAI_API_KEY")!);

var texts = new[]
{
    "The quick brown fox jumps over the lazy dog.",
    "Machine learning is a subset of artificial intelligence.",
    "Embeddings represent text as numerical vectors.",
};

var embeddings = await generator.GenerateAsync(
    texts,
    new EmbeddingGenerationOptions { ModelId = "voyage-3" });

Console.WriteLine($"Generated {embeddings.Count} embeddings");
Console.WriteLine($"Total tokens: {embeddings.Usage?.TotalTokenCount}");
```

## Custom Dimensions

Voyage models support custom output dimensions via `EmbeddingGenerationOptions.Dimensions`:

```csharp
using Microsoft.Extensions.AI;
using VoyageAI;

IEmbeddingGenerator<string, Embedding<float>> generator =
    new VoyageAIClient(apiKey: Environment.GetEnvironmentVariable("VOYAGEAI_API_KEY")!);

var options = new EmbeddingGenerationOptions
{
    ModelId = "voyage-3",
    Dimensions = 256, // Reduce from default
};

var embeddings = await generator.GenerateAsync(
    ["Compact embedding for efficient storage."], options);

Console.WriteLine($"Dimensions: {embeddings[0].Vector.Length}"); // 256
```

## Semantic Similarity

```csharp
using Microsoft.Extensions.AI;
using VoyageAI;

IEmbeddingGenerator<string, Embedding<float>> generator =
    new VoyageAIClient(apiKey: Environment.GetEnvironmentVariable("VOYAGEAI_API_KEY")!);

var options = new EmbeddingGenerationOptions { ModelId = "voyage-3" };
var embeddings = await generator.GenerateAsync([
    "How do I reset my password?",
    "I forgot my login credentials.",
    "What are your business hours?",
], options);

static float CosineSimilarity(ReadOnlySpan<float> a, ReadOnlySpan<float> b)
{
    float dot = 0, normA = 0, normB = 0;
    for (int i = 0; i < a.Length; i++)
    {
        dot += a[i] * b[i];
        normA += a[i] * a[i];
        normB += b[i] * b[i];
    }
    return dot / (MathF.Sqrt(normA) * MathF.Sqrt(normB));
}

var sim01 = CosineSimilarity(embeddings[0].Vector.Span, embeddings[1].Vector.Span);
var sim02 = CosineSimilarity(embeddings[0].Vector.Span, embeddings[2].Vector.Span);

Console.WriteLine($"'password reset' vs 'forgot credentials': {sim01:F4}");
Console.WriteLine($"'password reset' vs 'business hours': {sim02:F4}");
```

## Dependency Injection

```csharp
using Microsoft.Extensions.AI;
using VoyageAI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IEmbeddingGenerator<string, Embedding<float>>>(
    new VoyageAIClient(apiKey: builder.Configuration["VoyageAI:ApiKey"]!));
```

## Provider Metadata

```csharp
var metadata = generator.GetService<EmbeddingGeneratorMetadata>();
Console.WriteLine($"Provider: {metadata?.ProviderName}"); // "VoyageAIClient"
Console.WriteLine($"Endpoint: {metadata?.ProviderUri}");
```
