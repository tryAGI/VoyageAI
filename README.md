# VoyageAI

[![NuGet](https://img.shields.io/nuget/v/tryAGI.VoyageAI.svg)](https://www.nuget.org/packages/tryAGI.VoyageAI/)
[![Downloads](https://img.shields.io/nuget/dt/tryAGI.VoyageAI.svg)](https://www.nuget.org/packages/tryAGI.VoyageAI/)

Generated C# SDK for the [Voyage AI](https://www.voyageai.com/) API — high-quality text embeddings and reranking for search and RAG pipelines.

## Features
- Text and multimodal embeddings (voyage-3, voyage-code-3, voyage-large-2, etc.)
- Document reranking
- Fully generated from the official Voyage AI OpenAPI specification
- NativeAOT/trimming compatible
- Strong-named assembly

## Usage

```csharp
using VoyageAI;

var client = new VoyageAIClient("your-api-key");
```

## License

MIT
