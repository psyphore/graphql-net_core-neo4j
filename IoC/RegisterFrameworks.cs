using DataAccess;
using DataAccess.Interfaces;
using GraphQL;
using GraphQL.Http;
using GraphQL.Server;
using GraphQLCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.DTOs.Configuration;

namespace IoC
{
    internal static class RegisterFrameworks
    {
        public static void ConfigureFrameworks(this IServiceCollection services, IConfiguration configuration)
        {
            var auth0conf = configuration.GetSection("Auth0");
            services.Configure<Auth0>(auth0conf);

            var auth0 = auth0conf.Get<Auth0>();
            services.AddSingleton(auth0);

            var dbconf = configuration.GetSection("ConnectionStrings");
            services.Configure<Connection>(dbconf);

            var neo4j = dbconf.Get<Connection>();
            services.AddSingleton(neo4j);

            // Repository
            services.AddTransient<IRepository>(o => new Repository(neo4j));

            // GraphQL
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<IDocumentExecuter, DocumentExecuter>();
            services.AddScoped<IDocumentWriter, DocumentWriter>();

            // GraphQL Schema
            services.AddScoped<MainSchema>();

            services
                .AddGraphQL(o =>
                {
                    o.EnableMetrics = true;
                    o.ExposeExceptions = true;
                })
                //.AddSystemTextJson()
                .AddUserContextBuilder(hc => new GraphQLUserContext { User = hc.User })
                .AddGraphTypes(ServiceLifetime.Scoped);
        }
    }
}