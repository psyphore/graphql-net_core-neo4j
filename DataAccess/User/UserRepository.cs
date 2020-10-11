using DataAccess.Interfaces;
using Neo4j.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.User
{
    public class UserRepository : IUserRepository
    {
        private readonly IDictionary<string, string> _mutations;
        private readonly IDictionary<string, string> _queries;
        private readonly IRepository _repository;

        public UserRepository(IRepository repository)
        {
            _repository = repository;
            _queries = new UserQueries().Queries;
            _mutations = new UserQueries().Mutations;
        }

        public async Task<User> Add(User person)
        {
            var entity = await _repository.Write<User>(_mutations["UPDATE_USER"].Trim(), new Dictionary<string, object> { { "user", person } });
            return entity;
        }

        public async Task<IEnumerable<User>> All()
        {
            var x = new List<User>();
            const int First = 9999;
            const int Offset = 0;
            const string LABEL = "user";
            var param = new Dictionary<string, object>()
            {
                {
                    "offset", Offset
                },
                {
                    "first", First
                }
            };
            var query = _queries["GET_USERS"].Trim();
            var records = await _repository.Read(query, param);

            x.AddRange(records.Select(record => ProcessProps(record, LABEL)).ToList());

            return x;
        }

        public async Task<string> Delete(string id)
        {
            var entity = await _repository.Write<User>(_mutations["DEACTIVATE_USER"].Trim(), new Dictionary<string, object> { { "id", id } });
            return entity.Id;
        }

        public async Task<User> Get(string id)
        {
            var entity = await _repository.Read<User>(_queries["GET_USER"].Trim(), new Dictionary<string, object> { { "id", id } });
            return entity;
        }

        public async Task<User> Update(User person)
        {
            var entity = await _repository.Write<User>(_mutations["UPDATE_USER_2"].Trim(), new Dictionary<string, object> { { "user", person } });
            return entity;
        }

        private User ProcessProps(IRecord record, string label)
        {
            var props = JsonConvert.SerializeObject(record[label]);
            var person = JsonConvert.DeserializeObject<User>(props);

            return person;
        }
    }
}