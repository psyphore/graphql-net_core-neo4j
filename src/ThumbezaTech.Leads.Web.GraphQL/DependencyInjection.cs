using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

        .AddMutationType()
        .AddTypeExtension<ProductsMutation>()

        //.AddSubscriptionType()
        //.AddTypeExtension<OrderSubscription>()

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
