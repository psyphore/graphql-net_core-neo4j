using DataAccess.Interfaces;
using Models.DTOs.Configuration;
using Neo4j.Driver.V1;
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

        public ISession GetSession(AccessMode mode)
        {
            return _driver.Session(mode);
        }

        public async Task<T> Read<T>(string query, object parameters)
        {
            using (var session = GetSession(AccessMode.Read))
            {
                var trx = await session.ReadTransactionAsync(async tx =>
                {
                    var response = await tx.RunAsync(query, parameters);
                    return response.ToListAsync().As<T>();
                });
                return trx;
            }
        }

        public async Task<IList<IRecord>> Read(string query, IDictionary<string, object> parameters)
        {
            using (var session = GetSession(AccessMode.Read))
            {
                var s = await session.RunAsync(query, parameters);
                return await s.ToListAsync();

                //var trx = await session.ReadTransactionAsync(async tx =>
                //{
                //    var response = await tx.RunAsync(query, parameters);
                //    return response.ToListAsync();
                //});
                //return await trx;
            }
        }

        public async Task<T> Write<T>(string query, object parameters)
        {
            using (var session = GetSession(AccessMode.Write))
            {
                var trx = await session.WriteTransactionAsync(async tx =>
                {
                    var response = await tx.RunAsync(query, parameters);
                    return response.ToListAsync().As<T>();
                });
                return trx;
            }
        }

        public async Task CreateIndices(string[] labels)
        {
            if (labels != null && labels.Any())
            {
                var queries = labels.Select(l => string.Format("CREATE INDEX ON :{0}(id)", l)).ToArray();
                using (var session = GetSession(AccessMode.Write))
                {
                    foreach (var query in queries)
                    {
                        await session.RunAsync(query);
                    }
                }
            }
        }
    }
}