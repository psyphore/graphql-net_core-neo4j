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
            // /graphql
            app.UseGraphQL<MainSchema>();
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