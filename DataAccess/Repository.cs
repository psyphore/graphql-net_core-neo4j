using DataAccess.Interfaces;
using Neo4j.Driver.V1;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Repository : IRepository
    {
        private readonly IDriver _driver;

        public Repository(string uri, string username, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(username, password));
        }

        public ISession GetSession(AccessMode mode)
        {
            return _driver.Session(mode);
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }

        public async Task<T> Read<T>(string query, object parameters)
        {
            using (var session = GetSession(AccessMode.Read))
            {
                var trx = await session.ReadTransactionAsync(async tx =>
                {
                    var response = await tx.RunAsync(query, parameters);
                    return response.SingleAsync().As<T>();
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
                    return response.SingleAsync().As<T>();
                });
                return trx;
            }
        }
    }
}