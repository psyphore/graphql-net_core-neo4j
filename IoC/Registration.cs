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
            RegisterFrameworks.RegisterTypes(services, configuration);

            RegisterRepositories.RegisterTypes(services);

            RegisterBusinessServices.RegisterTypes(services);

            services.ConfigureGraphQLServices();
        }
    }
}