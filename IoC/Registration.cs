using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class Registration
    {
        public static IApplicationBuilder ConfigureApp(this IApplicationBuilder app)
        {
            app.UseGraphQL()
                .UseVoyager();

            return app;
        }

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureFrameworks(configuration);
            services.ConfigureRepositories();
            services.ConfigureServices();
            services.ConfigureGraphQLServices();

            return services;
        }
    }
}