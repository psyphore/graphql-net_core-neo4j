using Microsoft.Extensions.Logging;

using Neo4j.Driver;
using Neo4j.Driver.Experimental;

using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Infrastructure.Data.Common;


internal sealed class Repository<T> : IRepository<T> where T : class, IAggregateRoot
{
  private readonly ILogger<Repository<T>> _logger;
  private readonly IDriver _driver;
  private readonly Neo4JConfiguration _connection;

  public Repository(ILogger<Repository<T>> logger, IDriver driver, Neo4JConfiguration connection)
  {
    _logger = logger;
    _driver = driver;
    _connection = connection;
  }

  ~Repository() => Task.Run(() => _driver?.VerifyConnectivityAsync());

  public async ValueTask CreateIndicesAsync(string[] labels)
  {
    labels = !labels.Any() ? new[] { typeof(T).Name.ToString() } : labels;
    foreach (var query in labels.Select(l => string.Format("CREATE INDEX ON :{0}(id)", l)))
      await _driver.ExecutableQuery(query).ExecuteAsync();
  }

  private IAsyncSession GetSession(AccessMode mode) 
    => _driver.AsyncSession(o => o.WithDatabase(_connection.DatabaseName).WithDefaultAccessMode(mode));

  public async ValueTask<T> Read<T>(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default)
  {
    await using var session = GetSession(AccessMode.Read);
    var x = await ProcessTransactionAsync(session, query, parameters, cancellationToken);
    return x.As<T>();
  }

  public async ValueTask<IReadOnlyList<IRecord>> Read(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default)
  {
    await using var session = GetSession(AccessMode.Read);
    return await ProcessAsync(session, query, parameters, cancellationToken);
  }

  public async ValueTask<T> Write<T>(string query, object parameters, CancellationToken cancellationToken = default)
  {
    await using var session = GetSession(AccessMode.Write);
    var x = await ProcessWriteTransactionAsync(session, query, parameters, cancellationToken);
    return x.As<T>();
  }

  public async ValueTask<IReadOnlyList<IRecord>> Write(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default)
  {
    await using var session = GetSession(AccessMode.Write);
    return await ProcessWriteTransactionAsync(session, query, parameters, cancellationToken);
  }

  private async ValueTask<IReadOnlyList<IRecord>> ProcessAsync(IAsyncSession session, string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default)
  {
    try
    {
      var content = await session.ExecuteReadAsync(async tx =>
      {
        var cursor = await tx.RunAsync(query, parameters);
        return await cursor.ToListAsync(cancellationToken);
      });
      return content;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Exception occurred while processing");
      throw;
    }
  }

  private async ValueTask<IReadOnlyList<IRecord>> ProcessTransactionAsync(IAsyncSession session, string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default)
  {
    try
    {
      var content = await session.ExecuteReadAsync(async tx =>
      {
        var cursor = await tx.RunAsync(query, parameters);
        return await cursor.ToListAsync(cancellationToken);
      });
      return content;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Exception occurred while Processing a transaction");
      throw;
    }
  }

  private async ValueTask<IReadOnlyList<IRecord>> ProcessWriteTransactionAsync(IAsyncSession session, string query, object parameters, CancellationToken cancellationToken = default)
  {
    try
    {
      var content = await session.ExecuteReadAsync(async tx =>
      {
        var cursor = await tx.RunAsync(query, parameters);
        return await cursor.ToListAsync(cancellationToken);
      });
      return content;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Exception occurred while Writing a transaction");
      throw;
    }
  }
}
