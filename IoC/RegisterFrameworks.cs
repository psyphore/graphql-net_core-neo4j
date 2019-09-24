using DataAccess;
using DataAccess.Interfaces;
using GraphQL;
using GraphQL.Http;
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

            services.AddTransient(o =>
            {
                return new Repository(neo4j);
            });

            services.AddTransient<IRepository, Repository>();

            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();
        }
    }
}