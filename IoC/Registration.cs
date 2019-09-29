using GraphQL.Server;
using GraphQLCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class Registration
    {
        public static void ConfigureApp(this IApplicationBuilder app)
        {
            app.UseGraphQLAuth();
            app.UseGraphQL<MainSchema>();
            
            //app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings
            //{
            //    Path = "/graphql",
            //    BuildUserContext = ctx => new GraphQLUserContext
            //    {
            //        User = ctx.User
            //    },
            //    EnableMetrics = true
            //});
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureFrameworks(configuration);

            services.ConfigureRepositories();

            services.ConfigureServices();

            services.ConfigureGraphQLServices();
        }
    }
}