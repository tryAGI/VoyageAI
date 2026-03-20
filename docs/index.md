# VoyageAI

[![Nuget package](https://img.shields.io/nuget/vpre/VoyageAI)](https://www.nuget.org/packages/VoyageAI/)
[![dotnet](https://github.com/tryAGI/VoyageAI/actions/workflows/dotnet.yml/badge.svg?branch=main)](https://github.com/tryAGI/VoyageAI/actions/workflows/dotnet.yml)
[![License: MIT](https://img.shields.io/github/license/tryAGI/VoyageAI)](https://github.com/tryAGI/VoyageAI/blob/main/LICENSE)
[![Discord](https://img.shields.io/discord/1115206893015662663?label=Discord&logo=discord&logoColor=white&color=d82679)](https://discord.gg/Ca2xhfBf3v)

## Features 🔥
- Fully generated C# SDK based on [official Voyage AI OpenAPI specification](https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml) using [AutoSDK](https://github.com/HavenDV/AutoSDK)
- Same day update to support new features
- Updated and supported automatically if there are no breaking changes
- All modern .NET features - nullability, trimming, NativeAOT, etc.
- Microsoft.Extensions.AI `IEmbeddingGenerator` support

### Usage
```csharp
using VoyageAI;

using var client = new VoyageAIClient(apiKey);
```

### Microsoft.Extensions.AI

The SDK implements [`IEmbeddingGenerator`](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.ai.iembeddinggenerator-2):
```csharp
using VoyageAI;
using Microsoft.Extensions.AI;

IEmbeddingGenerator<string, Embedding<float>> generator = new VoyageAIClient(apiKey);

var embeddings = await generator.GenerateAsync(
    ["Hello, world!"],
    new EmbeddingGenerationOptions { ModelId = "voyage-3" });

Console.WriteLine($"Embedding dimension: {embeddings[0].Vector.Length}");
```

<!-- EXAMPLES:START -->
### Embedding Generator Generate Async


```csharp
using var client = GetAuthorizedApi();
IEmbeddingGenerator<string, Embedding<float>> generator = client;

var embeddings = await generator.GenerateAsync(
    ["Hello, world!"],
    new EmbeddingGenerationOptions
    {
        ModelId = "voyage-3-lite",
    });
```

### Embedding Generator Get Service Returns Metadata


```csharp
using var client = new VoyageAIClient("test-api-key");
IEmbeddingGenerator<string, Embedding<float>> generator = client;

var metadata = generator.GetService<EmbeddingGeneratorMetadata>();
```

### Embedding Generator Get Service Returns Self


```csharp
using var client = new VoyageAIClient("test-api-key");
IEmbeddingGenerator<string, Embedding<float>> generator = client;

var self = generator.GetService<VoyageAIClient>();
```

### Embeddings


```csharp
using var client = GetAuthorizedApi();

var response = await client.EmbeddingsApiAsync(
    input: "Hello, world!",
    model: "voyage-3-lite");
```

### Multimodal Embeddings Text And Image Url


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

### Multimodal Embeddings Text Only


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

### Reranking


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
<!-- EXAMPLES:END -->

## Support

Priority place for bugs: https://github.com/tryAGI/VoyageAI/issues
Priority place for ideas and general questions: https://github.com/tryAGI/VoyageAI/discussions
Discord: https://discord.gg/Ca2xhfBf3v

## Acknowledgments

![JetBrains logo](https://resources.jetbrains.com/storage/products/company/brand/logos/jetbrains.png)

This project is supported by JetBrains through the [Open Source Support Program](https://jb.gg/OpenSourceSupport).

![CodeRabbit logo](https://opengraph.githubassets.com/1c51002d7d0bbe0c4fd72ff8f2e58192702f73a7037102f77e4dbb98ac00ea8f/marketplace/coderabbitai)

This project is supported by CodeRabbit through the [Open Source Support Program](https://github.com/marketplace/coderabbitai).
