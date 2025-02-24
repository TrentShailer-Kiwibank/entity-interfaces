# Entity Interfaces

Sample code to reproduce HotChocolate's inability to register reference resolvers on entity interfaces.

This violates Apollo's documentation for entity interfaces:
* <https://www.apollographql.com/docs/graphos/schema-design/federated-schemas/entities/interfaces#example-schemas>
* <https://www.apollographql.com/docs/graphos/schema-design/federated-schemas/entities/interfaces#required-resolvers:~:text=Subgraph%20A%20needs,for%20Media>

This sample includes a subgraph that when federated, will encounter runtime errors.

## Minimum reproduction

1. `cd Pet`
1. `dotnet run`
1. <http://localhost:6000/graphql>
1. Query:
  ```graphql
  query {
      _entities(representations: [
          {
              __typename: "Pet"
              id: "0"
          }
      ]) {
          ... on Pet {
              birthDate
          }
      }
  }
   ```
1. Response:
  ```json
  {
    "errors": [
      {
        "message": "Unexpected Execution Error",
        "locations": [
          {
            "line": 2,
            "column": 5
          }
        ],
        "path": [
          "_entities"
        ],
        "extensions": {
          "message": "For more details look at the `Errors` property.\n\n1. The apollo gateway tries to resolve an entity for which no EntityResolver method was found.\n",
          "stackTrace": "   at HotChocolate.ApolloFederation.Resolvers.EntitiesResolver.ResolveAsync(ISchema schema, IReadOnlyList`1 representations, IResolverContext context)\n   at HotChocolate.Types.ResolveObjectFieldDescriptorExtensions.<>c__DisplayClass3_0`1.<<Resolve>b__0>d.MoveNext()\n--- End of stack trace from previous location ---\n   at HotChocolate.Types.Helpers.FieldMiddlewareCompiler.<>c__DisplayClass9_0.<<CreateResolverMiddleware>b__0>d.MoveNext()\n--- End of stack trace from previous location ---\n   at HotChocolate.Execution.Processing.Tasks.ResolverTask.ExecuteResolverPipelineAsync(CancellationToken cancellationToken)\n   at HotChocolate.Execution.Processing.Tasks.ResolverTask.TryExecuteAsync(CancellationToken cancellationToken)"
        }
      }
    ],
    "data": null
  }
  ```

## Federation reproduction

1. `cd Pet`
1. `dotnet run`
1. `cd Owner`
1. `dotnet run`
1. `cd ApolloRouter`
1. `./apollo-router.sh`
1. <localhost:4000>
1. Query:
  ```graphql
  query {
    owner(id: "0") {
      name
      pets {
        name
      }
    }
  }
  ```
1. Response:
  ```json
  {
    "data": {
        "owner": {
            "name": "Owner A",
            "pets": [
                {
                "name": null
                }
            ]
        }
    },
    "errors": [
        {
            "message": "Unexpected Execution Error",
            "path": [
                "owner",
                "pets",
                "@"
            ],
            "extensions": {
                "message": "For more details look at the `Errors` property.\n\n1. The apollo gateway tries to resolve an entity for which no EntityResolver method was found.\n",
                "stackTrace": "   at HotChocolate.ApolloFederation.Resolvers.EntitiesResolver.ResolveAsync(ISchema schema, IReadOnlyList`1 representations, IResolverContext context)\n   at HotChocolate.Types.ResolveObjectFieldDescriptorExtensions.<>c__DisplayClass3_0`1.<<Resolve>b__0>d.MoveNext()\n--- End of stack trace from previous location ---\n   at HotChocolate.Types.Helpers.FieldMiddlewareCompiler.<>c__DisplayClass9_0.<<CreateResolverMiddleware>b__0>d.MoveNext()\n--- End of stack trace from previous location ---\n   at HotChocolate.Execution.Processing.Tasks.ResolverTask.ExecuteResolverPipelineAsync(CancellationToken cancellationToken)\n   at HotChocolate.Execution.Processing.Tasks.ResolverTask.TryExecuteAsync(CancellationToken cancellationToken)",
                "service": "pet"
            }
        }
    ]
  }
  ```
