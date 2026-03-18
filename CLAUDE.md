# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

C# SDK for the [Voyage AI](https://www.voyageai.com/) embedding and reranking API, auto-generated from the Voyage AI OpenAPI specification using [AutoSDK](https://github.com/HavenDV/AutoSDK). Published as a NuGet package under the `tryAGI` organization.

## Build Commands

```bash
# Build the solution
dotnet build VoyageAI.slnx

# Build for release (also produces NuGet package)
dotnet build VoyageAI.slnx -c Release

# Run integration tests (requires VOYAGEAI_API_KEY env var)
dotnet test src/tests/VoyageAI.IntegrationTests/VoyageAI.IntegrationTests.csproj

# Regenerate SDK from OpenAPI spec
cd src/libs/VoyageAI && ./generate.sh
```

## Architecture

### Code Generation Pipeline

The SDK code is **entirely auto-generated** -- do not manually edit files in `src/libs/VoyageAI/Generated/`.

1. `src/libs/VoyageAI/openapi.yaml` -- the Voyage AI OpenAPI spec (fetched from `https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml`)
3. `src/libs/VoyageAI/generate.sh` -- orchestrates: download spec, fix spec, run AutoSDK CLI, output to `Generated/`
4. CI auto-updates the spec and creates PRs if changes are detected

### Hand-Written Extensions

| Path | Purpose |
|------|---------|
| `Extensions/VoyageAIClient.EmbeddingGenerator.cs` | MEAI `IEmbeddingGenerator<string, Embedding<float>>` implementation |
| `Extensions/MultimodalInput.cs` | Typed helpers for multimodal embeddings (text + image inputs) |
| `Extensions/RateLimitRetryHandler.cs` | HTTP 429 retry with exponential backoff |

### Project Layout

| Project | Purpose |
|---------|---------|
| `src/libs/VoyageAI/` | Main SDK library (`VoyageAIClient`) |
| `src/tests/VoyageAI.IntegrationTests/` | Integration tests against real Voyage AI API |

### Build Configuration

- **Target:** `net10.0` (single target)
- **Language:** C# preview with nullable reference types
- **Signing:** Strong-named assemblies via `src/key.snk`
- **Versioning:** Semantic versioning from git tags (`v` prefix) via MinVer
- **Analysis:** All .NET analyzers enabled, AOT/trimming compatibility enforced
- **Testing:** MSTest + FluentAssertions

### CI/CD

- Uses shared workflows from `HavenDV/workflows` repo
- Dependabot updates NuGet packages weekly (auto-merged)
- Documentation deployed to GitHub Pages via MkDocs Material
