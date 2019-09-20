using DataAccess.Interfaces;
using Models.DTOs.Configuration;
using Neo4j.Driver.V1;
using System.Collections.Generic;
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
                    var response = await tx.RunAsync(query,parameters);
                    return response.ToListAsync().As<T>();
                });
                return trx;
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
    }
}