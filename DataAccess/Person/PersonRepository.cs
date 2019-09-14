using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            var entity = await _repository.Write<Person>(_mutations["MERGE_PERSON"], person);
            return entity;
        }

        public async Task<IEnumerable<Person>> All()
        {
            var entity = await _repository.Read<IEnumerable<Person>>(_queries["GET_PEOPLE"], null);
            return entity;
        }

        public async Task<string> Delete(string id)
        {
            var entity = await _repository.Write<Person>(_mutations["DELETE_PERSON"], id);
            return entity.Id;
        }

        public async Task<Person> Get(string id)
        {
            var entity = await _repository.Read<Person>(_queries["GET_PERSON"], id);
            return entity;
        }

        public async Task<Person> Update(Person person)
        {
            var entity = await _repository.Write<Person>(_mutations["MERGE_PERSON"], person);
            return entity;
        }
    }
}