using Neo4j.Driver;

namespace ThumbezaTech.Leads.Infrastructure.Data.Common;

using ThumbezaTech.Leads.SharedKernel.Interfaces;

public interface IRepository<TAggregate> where TAggregate : IAggregateRoot
{
    ValueTask CreateIndicesAsync(string[] labels);
    ValueTask<IList<IRecord>> Read(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default);
    ValueTask<T> Read<T>(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default);
    ValueTask<IList<IRecord>> Write(string query, IDictionary<string, object> parameters, CancellationToken cancellationToken = default);
    ValueTask<T> Write<T>(string query, object parameters, CancellationToken cancellationToken = default);
}
