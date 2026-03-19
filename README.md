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
