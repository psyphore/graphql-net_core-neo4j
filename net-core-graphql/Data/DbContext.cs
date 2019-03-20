using Neo4j.Driver.V1;
using System;

namespace net_core_graphql.Data
{
    public class DbContext : IDisposable
    {
        private readonly IDriver _driver;

        public DbContext(string uri, string user, string password)
        {
            _driver = GraphDatabase.Driver(uri, AuthTokens.Basic(user, password));
        }

        public ISession Session { get { return _driver.Session(); } }

        public void Dispose()
        {
            _driver?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}