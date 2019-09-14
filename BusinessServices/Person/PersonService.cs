using BusinessServices.Person.Extensions;
using DataAccess.CacheProvider;
using DataAccess.Person;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessServices.Person
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository repository;
        private readonly ICacheProvider _cache;
        private const string CACHE_KEY_PREFIX = "PERSON_QUERY";

        public PersonService(IPersonRepository repository, ICacheProvider cache)
        {
            this.repository = repository;
            this._cache = cache;
        }

        public async Task<PersonModel> Get(string id)
        {
            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, id.ToUpper());
            var found = _cache.Fetch<PersonModel>(queryCacheKey);
            if (found != null)
            {
                return found;
            }

            var results = await repository.Get(id);
            if (results != null && _cache.Save(queryCacheKey, results))
            {
                return results.ToModel();
            }

            return null;
        }

        public async Task<PersonModel> Update(PersonModel person)
        {
            var result = await repository.Update(person.ToEnitity());

            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, result.Id.ToUpper());
            if (result != null && _cache.Save(queryCacheKey, result.ToModel()))
            {
                return result.ToModel();
            }

            return null;
        }

        public async Task<IEnumerable<PersonModel>> GetAll()
        {
            var result = await repository.All();
            return result.Select(r => r.ToModel());
        }

        public async Task<PersonModel> Create(PersonModel person)
        {
            var result = await repository.Add(person.ToEnitity());
            return result.ToModel();
        }

        public async Task<PersonModel> Add(PersonModel person)
        {
            var result = await repository.Add(person.ToEnitity());

            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, result.Id.ToUpper());
            if (result != null && _cache.Save(queryCacheKey, result.ToModel()))
            {
                return result.ToModel();
            }

            return null;
        }

        public async Task<PersonModel> Delete(string id)
        {
            var result = await repository.Delete(id);
            return new PersonModel { Id = id };
        }
    }
}