# GraphQL - .Net Core 3.1 + Neo4J 4.1
this is my take on GraphQL and .net core 3.1
stack: 
- .Net core 3.1
- Neo4j-driver 4.1 
- HotChocolate GraphQL 10.5

## Project Structure
```
net-core-graphql
|
|__ Models

|
|__ IoC
    |
    |__ GraphQLCore

    |
    |__ BusinessServices

    |
    |__ DataAccess
```

## Environment
```json
"ConnectionStrings": {
    "BoltURL": "bolt://localhost:7687",
    "Username": "neo4j",
    "Password": "n4j",
    "databaseName":  "neo4j" 
  },
```

## Learning resources
1. [GraphQL.NET](https://graphql-dotnet.github.io/)
1. [GraphQL Authorization](https://github.com/graphql-dotnet/authorization)
1. [Building GraphQL API](https://fullstackmark.com/post/17/building-a-graphql-api-with-aspnet-core-2-and-entity-framework-core)
1. [How to GraphQL](https://www.howtographql.com/)
1. [Neo4J .net driver](https://neo4j.com/developer/dotnet/)
1. [NetCore 3.0 GraphQL](https://dev.to/dotnet/learn-how-you-can-use-graphql-in-net-core-and-c-4h96) 
1. [GraphQL.NET + .NET Core](https://code-maze.com/graphql-aspnetcore-basics/)