#!/usr/bin/env bash
set -euo pipefail

readonly openapi_url="https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml"

dotnet tool update --global autosdk.cli --prerelease || dotnet tool install --global autosdk.cli --prerelease
rm -rf Generated
curl --fail --silent --show-error --location "$openapi_url" -o openapi.yaml

# Fix upstream security scheme: 'Authorization: Bearer' as apiKey header name is invalid.
# Replace with standard HTTP bearer auth. See https://github.com/voyage-ai/openapi/issues/1
sed -i.bak 's/type: apiKey/type: http/' openapi.yaml
sed -i.bak '/in: header/d' openapi.yaml
sed -i.bak "s/name: 'Authorization: Bearer'/scheme: bearer/" openapi.yaml
rm -f openapi.yaml.bak

autosdk generate openapi.yaml \
  --namespace VoyageAI \
  --clientClassName VoyageAIClient \
  --targetFramework net8.0 \
  --output Generated \
  --exclude-deprecated-operations
