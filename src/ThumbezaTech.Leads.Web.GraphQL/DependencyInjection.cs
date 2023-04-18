using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using StackExchange.Redis;

using ThumbezaTech.Leads.Web.GraphQL.Leads;
using ThumbezaTech.Leads.Web.GraphQL.Orders;
using ThumbezaTech.Leads.Web.GraphQL.Products;

namespace ThumbezaTech.Leads.Web.GraphQL;

public static class DependencyInjection
{
  public static IServiceCollection AddGraphQL(this IServiceCollection services)
  {
    services
        .AddGraphQLServer()
        .AddDefaultTransactionScopeHandler()

        .AddQueryType()
        .AddTypeExtension<ProductsQuery>()
        .AddTypeExtension<OrderQuery>()
        .AddTypeExtension<LeadQuery>()

        .AddMutationType()
        .AddTypeExtension<ProductsMutation>()
        .AddTypeExtension<OrderMutation>()
        .AddTypeExtension<LeadMutation>()

        .AddSubscriptionType()
        .AddRedisSubscriptions((sp) =>
        {
          var config = sp.GetRequiredService<IConfiguration>();
          return ConnectionMultiplexer.Connect($"{config["Redis:Host"]}:{config["Redis:Port"]}");
        })
        .AddTypeExtension<ProductSubscription>()
        .AddTypeExtension<OrderSubscription>()
        .AddTypeExtension<LeadSubscription>()

        .AddFiltering()
        .AddSorting()
        .AddProjections()
        ;

    return services;
  }

  public static IApplicationBuilder UseGraphQLResolver(this IApplicationBuilder app, IWebHostEnvironment env)
  {
    app.UseEndpoints(endpoints =>
    {
      _ = endpoints.MapGraphQL().WithOptions(new HotChocolate.AspNetCore.GraphQLServerOptions
      {
        EnableSchemaRequests = !env.IsProduction(),
        EnableBatching = true,
        EnableMultipartRequests = true,
        Tool =
            {
                    Enable = env.IsDevelopment(),
            }
      });
      endpoints.MapGet("/", context =>
          {
            context.Response.Redirect("/graphql", true);
            return Task.CompletedTask;
          });
    });

    return app;
  }
}
