#!/usr/bin/env bash
set -euo pipefail

# OpenAPI spec: https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml

dotnet tool install --global autosdk.cli --prerelease
rm -rf Generated
curl --fail --silent --show-error --location https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml -o openapi.yaml

# Auth: --security-scheme overrides the spec's apiKey auth with standard HTTP bearer.
# See https://github.com/voyage-ai/openapi/issues/1
autosdk generate openapi.yaml \
  --namespace VoyageAI \
  --clientClassName VoyageAIClient \
  --targetFramework net10.0 \
  --output Generated \
  --exclude-deprecated-operations \
  --security-scheme Http:Header:Bearer
