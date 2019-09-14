using DataAccess;
using GraphQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.DTOs.Configuration;

namespace IoC
{
    internal static class RegisterFrameworks
    {
        public static void RegisterTypes(IServiceCollection services, IConfiguration configuration)
        {
            var auth0conf = configuration.GetSection("Auth0");
            services.Configure<Auth0>(auth0conf);
            var auth0 = auth0conf.Get<Auth0>();

            services.AddSingleton(auth0);

            var dbconf = configuration.GetSection("ConnectionStrings");
            services.Configure<Connection>(dbconf);
            var neo4j = dbconf.Get<Connection>();

            services.AddSingleton(neo4j);

            services.AddTransient(o =>
            {
                return new Repository(neo4j.BoltURL, neo4j.Username, neo4j.Password);
            });

            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
        }
    }
}