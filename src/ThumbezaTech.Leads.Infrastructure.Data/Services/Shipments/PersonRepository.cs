namespace ThumbezaTech.Leads.Infrastructure.Data.Services.Shipments;
internal sealed class PersonRepository
{
  private readonly INeo4jDataAccess _neo4jDataAccess;

  /// <summary>
  /// Initializes a new instance of the <see cref="PersonRepository"/> class.
  /// </summary>
  public PersonRepository(INeo4jDataAccess neo4jDataAccess) => _neo4jDataAccess = neo4jDataAccess;

  /// <summary>
  /// Searches the name of the person.
  /// </summary>
  public async ValueTask<List<Dictionary<string, object>>> SearchPersonsByName(string searchString)
  {
    var query = @"MATCH (p:Person) WHERE toUpper(p.name) CONTAINS toUpper($searchString) 
                                RETURN p{ name: p.name, born: p.born } ORDER BY p.Name LIMIT 5";

    IDictionary<string, object> parameters = new Dictionary<string, object> { { "searchString", searchString } };

    var persons = await _neo4jDataAccess.ExecuteReadDictionaryAsync(query, "p", parameters);

    return persons;
  }

  /// <summary>
  /// Adds a new person
  /// </summary>
  public async ValueTask<bool> AddPerson(Person person)
  {
    if (person != null && !string.IsNullOrWhiteSpace(person.Name))
    {
      var query = @"MERGE (p:Person {name: $name}) ON CREATE SET p.born = $born 
                            ON MATCH SET p.born = $born, p.updatedAt = timestamp() RETURN true";
      IDictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "name", person.Name },
                    { "born", person.Born }
                };
      return await _neo4jDataAccess.ExecuteWriteTransactionAsync<bool>(query, parameters);
    }
    else
    {
      throw new ArgumentNullException(nameof(person), "Person must not be null");
    }
  }

  /// <summary>
  /// Get count of persons
  /// </summary>
  public async ValueTask<long> GetPersonCount()
  {
    var query = @"Match (p:Person) RETURN count(p) as personCount";
    var count = await _neo4jDataAccess.ExecuteReadScalarAsync<long>(query);
    return count;
  }
}

public record Person(string Name, DateOnly? Born);
