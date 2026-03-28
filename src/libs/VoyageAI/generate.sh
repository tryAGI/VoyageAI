#!/usr/bin/env bash
set -euo pipefail

readonly openapi_url="https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml"

dotnet tool install --global autosdk.cli --prerelease
rm -rf Generated
curl --fail --silent --show-error --location "$openapi_url" -o openapi.yaml

# Fix upstream security scheme: 'Authorization: Bearer' as apiKey header name is invalid.
# Replace with standard HTTP bearer auth. See https://github.com/voyage-ai/openapi/issues/1
yq -i '.components.securitySchemes.ApiKeyAuth = {"type": "http", "scheme": "bearer", "x-default": "$VOYAGE_API_KEY"}' openapi.yaml

autosdk generate openapi.yaml \
  --namespace VoyageAI \
  --clientClassName VoyageAIClient \
  --targetFramework net10.0 \
  --output Generated \
  --exclude-deprecated-operations
