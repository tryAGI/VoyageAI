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

rm -rf ../../cli/VoyageAI.CLI

autosdk cli-project openapi.yaml \
  --output ../../cli/VoyageAI.CLI \
  --sdk-project ../../libs/VoyageAI/VoyageAI.csproj \
  --targetFramework net10.0 \
  --namespace VoyageAI \
  --clientClassName VoyageAIClient \
  --package-id VoyageAI.CLI \
  --tool-command-name tryagi-voyage-ai \
  --user-secrets-id VoyageAI.CLI \
  --api-key-env-var VOYAGEAI_API_KEY \
  --base-url-env-var VOYAGEAI_BASE_URL \
  --cli-credential-file \
  --exclude-deprecated-operations \
  --security-scheme Http:Header:Bearer
