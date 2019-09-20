using Neo4j.Driver.V1;
using System;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository : IDisposable
    {
        Task<T> Read<T>(string query, object parameters);

        Task<T> Write<T>(string query, object parameters);

        ISession GetSession(AccessMode mode);
        Task CreateIndices(string[] labels);
        Task<System.Collections.Generic.IList<IRecord>> Read(string query, System.Collections.Generic.IDictionary<string, object> parameters);
    }
}