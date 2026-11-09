#!/usr/bin/env bash
set -euo pipefail

install_autosdk_cli() {
  dotnet tool update --global autosdk.cli --prerelease >/dev/null 2>&1 || \
    dotnet tool install --global autosdk.cli --prerelease
}

fetch_spec() {
  curl "$@" \
    --fail --silent --show-error --location \
    --retry 5 --retry-delay 10 --retry-all-errors \
    --connect-timeout 30 --max-time 300
}

# OpenAPI spec: https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml
# --reject-unknown-enum-values is only safe on a build that narrows it to enums which cannot appear in
# a response; an older one accepts the flag and hardens every enum, so ordinary replies start throwing.
# `autosdk capabilities --require` asks the build itself, which replaces both a version floor (had to be
# looked up after each publish, and moved once already) and a grep over help-text prose. A CLI without
# the subcommand fails the call, so this is fail-closed.
require_autosdk_capability() {
  if ! autosdk capabilities --require "$1" >/dev/null 2>&1; then
    echo "error: this autosdk.cli does not support '$1'." >&2
    echo "       Run: dotnet tool update --global autosdk.cli --prerelease" >&2
    exit 1
  fi
}
install_autosdk_cli
require_autosdk_capability behavior:request-scoped-enum-rejection
rm -rf Generated
fetch_spec --fail --silent --show-error --location https://raw.githubusercontent.com/voyage-ai/openapi/main/voyage-openapi.yml -o openapi.yaml

# Fix broken auth: spec uses name "Authorization: Bearer" which is invalid as a header name.
# Remove the broken apiKey scheme so --security-scheme is the only auth definition.
# See https://github.com/voyage-ai/openapi/issues/1
yq -i 'del(.components.securitySchemes) | del(.security)' openapi.yaml
autosdk generate openapi.yaml \
  --namespace VoyageAI \
  --clientClassName VoyageAIClient \
  --targetFramework net10.0 \
  --output Generated \
  --reject-unknown-enum-values \
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
  --tool-command-name voyage-ai \
  --user-secrets-id VoyageAI.CLI \
  --api-key-env-var VOYAGEAI_API_KEY \
  --base-url-env-var VOYAGEAI_BASE_URL \
  --cli-credential-file \
  --exclude-deprecated-operations \
  --security-scheme Http:Header:Bearer
