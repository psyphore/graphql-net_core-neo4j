using Microsoft.Extensions.Logging;

using Neo4j.Driver;

namespace ThumbezaTech.Leads.Infrastructure.Data.Common;

// https://github.com/chintan196/DotnetCore.Neo4j

internal sealed class Neo4jDataAccess : INeo4jDataAccess
{
  private readonly IAsyncSession _session;
  private readonly ILogger<Neo4jDataAccess> _logger;

  /// <summary>
  /// Initializes a new instance of the <see cref="Neo4jDataAccess"/> class.
  /// </summary>
  public Neo4jDataAccess(IDriver driver, ILogger<Neo4jDataAccess> logger, Neo4JConfiguration connection)
  {
    _logger = logger;
    _session = driver.AsyncSession(o => o.WithDatabase(connection.DatabaseName));
  }

  /// <summary>
  /// Execute read list as an asynchronous operation.
  /// </summary>
  public async ValueTask<List<string>> ExecuteReadListAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null)
    => await ExecuteReadTransactionAsync<string>(query, returnObjectKey, parameters);

  /// <summary>
  /// Execute read dictionary as an asynchronous operation.
  /// </summary>
  public async ValueTask<List<Dictionary<string, object>>> ExecuteReadDictionaryAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null)
    => await ExecuteReadTransactionAsync<Dictionary<string, object>>(query, returnObjectKey, parameters);

  /// <summary>
  /// Execute read scalar as an asynchronous operation.
  /// </summary>
  public async ValueTask<T> ExecuteReadScalarAsync<T>(string query, IDictionary<string, object>? parameters = null)
  {
    try
    {
      parameters ??= new Dictionary<string, object>();
      return await _session.ExecuteReadAsync(async tx =>
      {
        T scalar = default;
        var res = await tx.RunAsync(query, parameters);
        return (await res.SingleAsync())[0].As<T>();
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "There was a problem while executing database query.");
      throw;
    }
  }

  /// <summary>
  /// Execute write transaction
  /// </summary>
  public async ValueTask<T> ExecuteWriteTransactionAsync<T>(string query, IDictionary<string, object>? parameters = null)
  {
    try
    {
      parameters ??= new Dictionary<string, object>();
      return await _session.ExecuteWriteAsync(async tx =>
      {
        T scalar = default;
        var res = await tx.RunAsync(query, parameters);
        return (await res.SingleAsync())[0].As<T>();
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "There was a problem while executing database query.");
      throw;
    }
  }

  /// <summary>
  /// Execute read transaction as an asynchronous operation.
  /// </summary>
  public async ValueTask<List<T>> ExecuteReadTransactionAsync<T>(string query, string returnObjectKey, IDictionary<string, object>? parameters)
  {
    try
    {
      parameters ??= new Dictionary<string, object>();
      return await _session.ExecuteReadAsync(async tx =>
      {
        var res = await tx.RunAsync(query, parameters);
        var records = await res.ToListAsync();
        return records.Select(x => x.ProcessRecords<T>(returnObjectKey)).ToList();
      });
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "There was a problem while executing database query.");
      throw;
    }
  }

  /// <summary>
  /// Performs application-defined tasks associated with freeing, releasing, or
  /// resetting unmanaged resources asynchronously.
  /// </summary>
  async ValueTask IAsyncDisposable.DisposeAsync() => await _session.CloseAsync();
}
