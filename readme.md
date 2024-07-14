# GraphQL - .Net 8 + Neo4J 5.22

This is my 2nd take on GraphQL with dotnet 8 and Neo4J Graph Database
I will loosely follow the Domain Driven Design pattern.

stack:

- dotnet 8 (LTS)
- Neo4j-driver 5.22
- HotChocolate GraphQL 13.9.7
- RedisStack 6.2.6v14

## Project Structure

```text
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
            |__ .Leads.Infrastructure.Email
            |
            |__ .Leads.Web.GraphQL
        |
        |__ .Leads.Web
```

## Environment

- Server Side Environment

  ```json
    {
      "Neo4J": {
        "BoltURL": "neo4j://localhost:7687",
        "Username": "lead-user",
        "Password": "thumbeza-tech-l3ad5",
        "databaseName": "leads"
      },
      "Redis": "localhost:6379",
      "Smtp": {
        "Sender": "no-reply@leads.thumbezatech.co.za",
        "SenderName": "Thumbeza Tech",
        "SmtpServer": "localhost",
        "Port": 1025
      }
    }
  ```

- Client Side Environment

    ```json
        {
          "LeadsGraphQLClient": {
            "Url": "https://localhost:7234/graphql/",
            "Ws": "wss://localhost:7234/graphql/",
            "Key": "Blazor Web Server"
          }
        }
    ```

## Tips

- Some things regarding StrawberryShake.Blazor are not documented well to understand the flow,
- So to get the solution working on client side,
  - if you have made changes to the graphql schema (e.g. introduced metadata to your handlers), you need to update the client i.e. Generated Client
  - update the schema
  
  - ```sh
    $> dotnet graphql update -u https://localhost:7234/graphql/ -p ./Client -j
    ```
  
  - create a sample call on Banana cake pop client, test your query, mutation or subs
  - create a .graphql file in the ClientLibrary/Client based on your test
  - build to generate updated client
  
  - ```sh
    $> dotnet build
    ```

## Learning resources

1. [How to GraphQL](https://www.howtographql.com/)
1. [Hot Chocolate](https://chillicream.com/docs/hotchocolate/v13/get-started-with-graphql-in-net-core)
1. [Neo4J dotnet driver](https://neo4j.com/developer/dotnet/)
1. [Awesome Blazor Browser Resources](https://jsakamoto.github.io/awesome-blazor-browser/?k=)
