using Neo4j.Driver;

using ThumbezaTech.Leads.SharedKernel.Interfaces;

namespace ThumbezaTech.Leads.Infrastructure.Data.Common;

public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
{
  ValueTask CreateIndicesAsync(string[] labels);
  ValueTask<IReadOnlyList<IRecord>> Read(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default);
  ValueTask<T> Read<T>(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default);
  ValueTask<IReadOnlyList<IRecord>> Write(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default);
  ValueTask<T> Write<T>(string query, object parameters, CancellationToken cancellationToken = default);
}
