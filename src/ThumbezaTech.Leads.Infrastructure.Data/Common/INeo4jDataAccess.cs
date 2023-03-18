namespace ThumbezaTech.Leads.Infrastructure.Data.Common;

public interface INeo4jDataAccess : IAsyncDisposable
{
  ValueTask<List<string>> ExecuteReadListAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null);
  ValueTask<List<Dictionary<string, object>>> ExecuteReadDictionaryAsync(string query, string returnObjectKey, IDictionary<string, object>? parameters = null);
  ValueTask<T> ExecuteReadScalarAsync<T>(string query, IDictionary<string, object>? parameters = null);
  ValueTask<List<T>> ExecuteReadTransactionAsync<T>(string query, string returnObjectKey, IDictionary<string, object>? parameters);
  ValueTask<T> ExecuteWriteTransactionAsync<T>(string query, IDictionary<string, object>? parameters = null);
}
