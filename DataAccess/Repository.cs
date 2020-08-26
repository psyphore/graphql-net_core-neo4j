using DataAccess.Interfaces;
using Models.DTOs.Configuration;
using Neo4j.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository : IRepository
    {
        private readonly IDriver _driver;

        public Repository(Connection connection)
        {
            _driver = GraphDatabase.Driver(connection.BoltURL, AuthTokens.Basic(connection.Username, connection.Password));
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }

        public IAsyncSession GetSession(AccessMode mode)
        {
            return _driver.AsyncSession();
        }

        public async Task<T> Read<T>(string query, object parameters)
        {
            var session = GetSession(AccessMode.Read);
            var trx = await session.ReadTransactionAsync(async tx =>
            {
                var response = await tx.RunAsync(query, parameters);
                return response.ToListAsync().As<T>();
            });
            await session.CloseAsync();
            return trx;
        }

        public async Task<IList<IRecord>> Read(string query, IDictionary<string, object> parameters)
        {
            var session = GetSession(AccessMode.Read);
            var s = await session.RunAsync(query, parameters);
            await session.CloseAsync();
            return await s.ToListAsync();
        }

        public async Task<T> Write<T>(string query, object parameters)
        {
            var session = GetSession(AccessMode.Write);
            var trx = await session.WriteTransactionAsync(async tx =>
            {
                var response = await tx.RunAsync(query, parameters);
                return response.ToListAsync().As<T>();
            });
            await session.CloseAsync();
            return trx;
        }

        public async Task CreateIndicesAsync(string[] labels)
        {
            if (labels != null && labels.Any())
            {
                var queries = labels.Select(l => string.Format("CREATE INDEX ON :{0}(id)", l)).ToArray();
                var session = GetSession(AccessMode.Write);
                foreach (var query in queries)
                {
                    await session.RunAsync(query);
                }

                await session.CloseAsync();
            }
        }
    }
}