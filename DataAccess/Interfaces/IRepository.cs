using Neo4j.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository : IDisposable
    {
        Task<T> Read<T>(string query, IDictionary<string, object> parameters);

        Task<T> Write<T>(string query, object parameters);

        IAsyncSession GetSession(AccessMode mode);
        Task CreateIndicesAsync(string[] labels);
        Task<IList<IRecord>> Read(string query, IDictionary<string, object> parameters);
    }
}