using System.Net.Http.Json;

using FluentAssertions;

using Neo4j.Driver;

using ThumbezaTech.Leads.Domain.LeadAggregate;
using ThumbezaTech.Leads.Infrastructure.Data.Common;
using ThumbezaTech.Leads.Infrastructure.Data.Services.Leads;

namespace ThumbezaTech.Leads.IntegrationTests;

public class CreateLeadFeature : IClassFixture<ApiFactory>
{
  private readonly HttpClient _client;
  private readonly ValidateLeadCreation _leadValidator;

  public CreateLeadFeature(ApiFactory factory)
  {
    _client = factory.CreateClient();
    _leadValidator = factory.LeadValidator;
  }

  [Fact]
  public async Task Should_publish_event_when_lead_created()
  {
    
    var mutation = """
      {
        "mutation": "create_lead",
        "variables": { 
                        "FirstName": "Sipho",
                        "LastName": "Hlophe"
                     }
      }
      """;

    var response = await _client.PostAsJsonAsync("/graphql", mutation).ConfigureAwait(true);
    var leadId = await response.Content.ReadAsStringAsync().ConfigureAwait(true);

    leadId.Should().NotBeNullOrEmpty();
    var result = await _leadValidator.ValidateCreationAsync(leadId)
      .ConfigureAwait(true);

    result.Should().BeTrue();
  }
}

public class ValidateLeadCreation
{
  private readonly INeo4jDataAccess _data;
  private readonly IDriver _factory;
  private const string Label = nameof(Lead);

  public ValidateLeadCreation(string connectionString)
  {
    _factory = GraphDatabase.Driver(new Uri(connectionString), builder =>
          builder
          .WithConnectionIdleTimeout(TimeSpan.FromSeconds(30))
          .WithConnectionTimeout(TimeSpan.FromSeconds(120))
          .WithEncryptionLevel(EncryptionLevel.None)
        );
  }

  public async ValueTask<bool> ValidateCreationAsync(string leadId)
  {
    Dictionary<string, object> Query = new()
    {
      ["id"] = leadId,
    };
    var statement = Queries.Options[Queries.GetOne].Trim();
    var payload = await _data.ExecuteReadTransactionAsync<Lead>(statement, Label, Query)
      .ConfigureAwait(true);

    return payload is not null && payload.Count == 1;
  }
}
