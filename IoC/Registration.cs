using GraphQLCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Types;

namespace IoC
{
    public static class Registration
    {
        public static void RegisterTypes(IServiceCollection services, IConfiguration configuration)
        {
            RegisterFrameworks.RegisterTypes(services, configuration);

            RegisterRepositories.RegisterTypes(services);

            RegisterBusinessServices.RegisterTypes(services);

            RegisterGraphQLHandlers.RegisterTypes(services);
        }

        public static void ConfigureApp(IApplicationBuilder app)
        {
            app.UseMiddleware<GraphQLMiddleware>(new GraphQLSettings
            {
                Path = "/graphql",
                BuildUserContext = ctx => new GraphQLUserContext
                {
                    User = ctx.User
                },
                EnableMetrics = true
            });
        }
    }
}