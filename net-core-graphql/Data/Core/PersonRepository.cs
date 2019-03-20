using net_core_graphql.Data.Interfaces;
using net_core_graphql.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_graphql.Data.Core
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _uri;
        private readonly string _user;
        private readonly string _password;
        private readonly DbContext _db;


        public PersonRepository(DbContext db)
        {
            _uri = "bolt://localhost:7687";
            _user = "neo4j";
            _password = "n4j";

            _db = db;
        }

        public Task<Person> Add(Person person)
        {
            var query = "CREATE (p:Person) SET p = $p RETURN p";

            using (var dbContext = new DbContext(_uri, _user, _password))
            using (var session = dbContext.Session)
            {
                var greeting = session.WriteTransaction(tx =>
                {
                    var result = tx.Run(query, new { person });
                    return result.FirstOrDefault().Values;
                });
            }

            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Person>> All()
        {
            var query = "MATCH (p:Person) RETURN p";

            using (var dbContext = new DbContext(_uri, _user, _password))
            using (var session = dbContext.Session)
            {
                var data = await session.RunAsync(query);
            }
            throw new System.NotImplementedException();
        }

        public async Task<Person> Get(string id)
        {
            var query = "MATCH (p:Person {id = $id}) RETURN p";

            using (var dbContext = new DbContext(_uri, _user, _password))
            using (var session = dbContext.Session)
            {
                var data = await session.RunAsync(query, new { id });
                var value = await data.ConsumeAsync();
            }
            throw new System.NotImplementedException();
        }
    }
}