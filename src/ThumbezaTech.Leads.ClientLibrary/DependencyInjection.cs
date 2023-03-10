using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ThumbezaTech.Leads.ClientLibrary;

public static class DependencyInjection
{
  public static IServiceCollection AddLeadsGraphQLClient(this IServiceCollection services, IConfigurationSection section)
  {
    ClientConfiguration config = new();
    section.Bind(config);

    services.AddSingleton(config);

    services
      .AddLeadsClient(StrawberryShake.ExecutionStrategy.CacheFirst)
      .ConfigureHttpClient(client =>
      {
        client.BaseAddress = new Uri(config.Url);
        client.DefaultRequestHeaders.Add("X-Client-Key", config.Key);
      });

    return services;
  }
}

public sealed record ClientConfiguration
{
  public string Url { get; set; } = default!;
  public string Key { get; set; } = default!;
}
