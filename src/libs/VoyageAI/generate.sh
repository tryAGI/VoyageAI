#!/usr/bin/env bash
set -euo pipefail

readonly openapi_url="https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml"

dotnet tool update --global autosdk.cli --prerelease || dotnet tool install --global autosdk.cli --prerelease
rm -rf Generated
curl --fail --silent --show-error --location "$openapi_url" -o openapi.yaml
autosdk generate openapi.yaml \
  --namespace VoyageAI \
  --clientClassName VoyageAIClient \
  --targetFramework net8.0 \
  --output Generated \
  --exclude-deprecated-operations
