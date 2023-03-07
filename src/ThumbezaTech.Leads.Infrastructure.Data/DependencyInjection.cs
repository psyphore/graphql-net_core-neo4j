using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Neo4j.Driver;

using ThumbezaTech.Leads.Application.Products;
using ThumbezaTech.Leads.Infrastructure.Data.Services.Products;

namespace ThumbezaTech.Leads.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection services, IConfigurationSection section)
    {
        Neo4JConfiguration conf = new();
        section.Bind(conf);

        services
            .AddSingleton(sp => conf)
            .AddSingleton(sp => GraphDatabase.Driver(conf.BoltURL, AuthTokens.Basic(conf.Username, conf.Password)));

        services.AddScoped(typeof(Common.IRepository<>), typeof(Common.Repository<>));

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}
