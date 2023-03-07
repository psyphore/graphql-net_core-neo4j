using Microsoft.Extensions.Logging;

using Neo4j.Driver;

using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Infrastructure.Data.Common;


public sealed class Repository<T> : IRepository<T> where T : class, IAggregateRoot
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

    var queries = labels.Select(l => string.Format("CREATE INDEX ON :{0}(id)", l)).ToArray();
    var session = GetSession(AccessMode.Write);
    foreach (var query in queries) await session.RunAsync(query);

    await session.CloseAsync();
  }

  private IAsyncSession GetSession(AccessMode mode) => _driver.AsyncSession(o => o.WithDatabase(_connection.DatabaseName).WithDefaultAccessMode(mode));

  public async ValueTask<T> Read<T>(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default)
  {
    var x = await ProcessTransactionAsync(GetSession(AccessMode.Read), query, parameters, cancellationToken);
    return x.As<T>();
  }

  public async ValueTask<IList<IRecord>> Read(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default) =>
      await ProcessAsync(GetSession(AccessMode.Read), query, parameters, cancellationToken);

  public async ValueTask<T> Write<T>(string query, object parameters, CancellationToken cancellationToken = default)
  {
    var x = await ProcessWriteTransactionAsync(GetSession(AccessMode.Write), query, parameters, cancellationToken);
    return x.As<T>();
  }

  public async ValueTask<IList<IRecord>> Write(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default) =>
      await ProcessWriteTransactionAsync(GetSession(AccessMode.Write), query, parameters, cancellationToken);

  private async ValueTask<List<IRecord>> ProcessAsync(IAsyncSession session, string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default)
  {
    try
    {
      var cursor = await session.RunAsync(query, parameters);
      return await cursor.ToListAsync(cancellationToken: cancellationToken);
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Exception occurred while processing");
      throw;
    }
    finally
    {
      await session.CloseAsync();
    }
  }

  private async ValueTask<List<IRecord>> ProcessTransactionAsync(IAsyncSession session, string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default)
  {
    try
    {
      var trx = await session.ExecuteReadAsync(tx => tx.RunAsync(query, parameters));
      return await trx.ToListAsync(cancellationToken: cancellationToken);
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Exception occurred while Processing a transaction");
      throw;
    }
    finally
    {
      await session.CloseAsync();
    }
  }

  private async ValueTask<List<IRecord>> ProcessWriteTransactionAsync(IAsyncSession session, string query, object parameters, CancellationToken cancellationToken = default)
  {
    try
    {
      var records = await session.ExecuteWriteAsync(tx =>
      {
        var cursor = tx.RunAsync(query, parameters);
        var r = cursor.Result.ToListAsync(cancellationToken: cancellationToken);
        return r;
      });
      return records;
    }
    catch (Exception e)
    {
      _logger.LogError(e, "Exception occurred while Writing a transaction");
      throw;
    }
    finally
    {
      await session.CloseAsync();
    }
  }
}
