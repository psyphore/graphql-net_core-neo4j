# GraphQL - .Net 6 + Neo4J 5.5

This is my 2nd take on GraphQL with dotnet 6 and Neo4J Graph Database
I will loosely follow the Domain Driven Design pattern.

stack:
- dotnet 6 (LTS)
- Neo4j-driver 5.5
- HotChocolate GraphQL 13.0.5

## Project Structure
```
net-core-graphql
|
|__ .Leads.SharedKernel
    |
    |__ .Leads.Domain
        |
        |__ .Leads.Application
            |
            |__ .Leads.Infrastructure.Data
            |
            |__ .Leads.Web.GraphQL
        |
        |__ .Leads.Web
```

## Environment
```json
{
  "Neo4J": {
    "BoltURL": "bolt://localhost:7687",
    "Username": "lead-user",
    "Password": "thumbeza-tech-l3ad5",
    "databaseName": "leads"
  }
}
```

## Learning resources

1. [How to GraphQL](https://www.howtographql.com/)
1. [Hot Chocolate](https://chillicream.com/docs/hotchocolate/v13/get-started-with-graphql-in-net-core)
1. [Neo4J dotnet driver](https://neo4j.com/developer/dotnet/)
1. [Awesome Blazor Browser Resources](https://jsakamoto.github.io/awesome-blazor-browser/?k=)