using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ThumbezaTech.Leads.ClientLibrary;

public static class DependencyInjection
{
  public static IServiceCollection AddClient(this IServiceCollection services, IConfigurationSection section)
  {
    ClientConfiguration config = new();
    section.Bind(config);

    services.AddSingleton(config);

    //services
    //  .AddLeadsClient()
    //  .ConfigureHttpClient(client =>
    //  {
    //    client.BaseAddress = new Uri(config.Url);
    //  });

    return services;
  }
}

public sealed record ClientConfiguration
{
  public string Url { get; set; }
  public string Key { get; set; }
}
