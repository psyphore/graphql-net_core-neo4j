using DotNet.Testcontainers.Builders;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

using Neo4j.Driver;

using Testcontainers.Neo4j;

namespace ThumbezaTech.Leads.IntegrationTests;
public class ApiFactory : WebApplicationFactory<IApiAssemblyMarker>, IAsyncLifetime
{
  private readonly Neo4jContainer _neo4JContainer = new Neo4jBuilder()
    .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(7687))
    .Build();
  public ValidateLeadCreation LeadValidator { get; private set; }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureTestServices(services =>
    {
      services.Remove<IDriver>();
      services.AddSingleton(sp => GraphDatabase.Driver(new Uri(_neo4JContainer.GetConnectionString()), builder =>
          builder
          .WithConnectionIdleTimeout(TimeSpan.FromSeconds(30))
          .WithConnectionTimeout(TimeSpan.FromSeconds(120))
          .WithEncryptionLevel(EncryptionLevel.None)
        ));
    });

    base.ConfigureWebHost(builder);
  }

  public async Task InitializeAsync()
  {
    await _neo4JContainer.StartAsync().ConfigureAwait(true);
    LeadValidator = new ValidateLeadCreation(_neo4JContainer.GetConnectionString());
  }

  Task IAsyncLifetime.DisposeAsync()
  {
    return _neo4JContainer.StopAsync();
  }
}

internal static class ServiceCollectionExtensions
{
  public static void Remove<T>(this IServiceCollection services)
  {
    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
    if (descriptor != null) services.Remove(descriptor);
  }
}
