#!/bin/bash

dotnet run --project ../Pet -- schema export --output schema.graphql
dotnet run --project ../Owner -- schema export --output schema.graphql

rover supergraph compose --config ./supergraph.yaml --output supergraph.graphql

docker run \
    --mount "type=bind,src=./supergraph.graphql,dst=/dist/schema/local.graphql" \
    --mount "type=bind,src=./router.yaml,dst=/dist/config/router.yaml" \
    --mount "type=bind,src=./plugin.rhai,dst=/rhai/scripts/plugin.rhai" \
    --net=host \
    --rm \
    ghcr.io/apollographql/router:v1.59.0 \
        --anonymous-telemetry-disabled \
        --log warn \
        -c config/router.yaml \
        -s schema/local.graphql
