#!/usr/bin/env bash
set -euo pipefail

# OpenAPI spec: https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml

dotnet tool install --global autosdk.cli --prerelease
rm -rf Generated
curl --fail --silent --show-error --location https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml -o openapi.yaml

# Fix broken auth: spec uses name "Authorization: Bearer" which is invalid as a header name.
# Remove the broken apiKey scheme so --security-scheme is the only auth definition.
# See https://github.com/voyage-ai/openapi/issues/1
yq -i 'del(.components.securitySchemes) | del(.security)' openapi.yaml
autosdk generate openapi.yaml \
  --namespace VoyageAI \
  --clientClassName VoyageAIClient \
  --targetFramework net10.0 \
  --output Generated \
  --exclude-deprecated-operations \
  --security-scheme Http:Header:Bearer
