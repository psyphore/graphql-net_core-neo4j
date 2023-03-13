using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Neo4j.Driver;

using ThumbezaTech.Leads.Application.Leads;
using ThumbezaTech.Leads.Application.Products;
using ThumbezaTech.Leads.Infrastructure.Data.Services.Leads;
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
        .AddSingleton(sp => GraphDatabase.Driver(new Uri(conf.BoltURL), AuthTokens.Basic(conf.Username, conf.Password), builder =>
          builder
          .WithConnectionIdleTimeout(TimeSpan.FromSeconds(30))
          .WithConnectionTimeout(TimeSpan.FromSeconds(120))
          .WithEncryptionLevel(EncryptionLevel.None)
        ));

    services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ILeadService, LeadService>();

    return services;
  }
}
