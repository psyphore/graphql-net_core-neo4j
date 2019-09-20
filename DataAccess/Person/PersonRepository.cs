using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Neo4j.Driver.V1;
using Newtonsoft.Json;

namespace DataAccess.Person
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDictionary<string, string> _mutations;
        private readonly IDictionary<string, string> _queries;
        private readonly IRepository _repository;

        public PersonRepository(IRepository repository)
        {
            _repository = repository;
            _queries = new PersonQueries().Queries;
            _mutations = new PersonQueries().Mutations;
        }

        public async Task<Person> Add(Person person)
        {
            var entity = await _repository.Write<Person>(_mutations["UPDATE_PERSON"].Trim(), person);
            return entity;
        }

        public async Task<IEnumerable<Person>> All()
        {
            var x = new List<Person>();
            var entity = await _repository.Read<object>(_queries["GET_PEOPLE"].Trim(), new { first = 9999, offset = 0 });
            if (entity != null)
                x = JsonConvert.DeserializeObject<List<Person>>(JsonConvert.SerializeObject(entity));

            return x;
        }

        public async Task<string> Delete(string id)
        {
            var entity = await _repository.Write<Person>(_mutations["DEACTIVATE_PERSON"].Trim(), new { id });
            return entity.Id;
        }

        public async Task<Person> Get(string id)
        {
            var entity = await _repository.Read<Person>(_queries["GET_PERSON"].Trim(), new { id });
            return entity;
        }

        public async Task<Person> Update(Person person)
        {
            var entity = await _repository.Write<Person>(_mutations["UPDATE_PERSON_2"].Trim(), person);
            return entity;
        }
    }
}