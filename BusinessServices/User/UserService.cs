using BusinessServices.User.Extensions;
using DataAccess.CacheProvider;
using DataAccess.User;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessServices.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;
        private readonly ICacheProvider cache;
        private const string CACHE_KEY_PREFIX = "USER_QUERY";

        public UserService(IUserRepository repository, ICacheProvider cache)
        {
            this.repository = repository;
            this.cache = cache;
        }

        public async Task<UserModel> Get(string id)
        {
            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, id.ToUpper());
            var found = cache.Fetch<UserModel>(queryCacheKey);
            if (found != null)
            {
                return found;
            }

            var results = await repository.Get(id);
            if (results != null && cache.Save(queryCacheKey, results))
            {
                return results.ToModel();
            }

            return null;
        }

        public async Task<UserModel> Update(UserModel person)
        {
            var result = await repository.Update(person.ToEnitity());

            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, result.Id.ToUpper());
            if (result != null && cache.Save(queryCacheKey, result.ToModel()))
            {
                return result.ToModel();
            }

            return null;
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var result = await repository.All();
            return result.Select(r => r.ToModel());
        }

        public async Task<UserModel> Create(UserModel person)
        {
            var result = await repository.Add(person.ToEnitity());
            return result.ToModel();
        }

        public async Task<UserModel> Add(UserModel person)
        {
            var result = await repository.Add(person.ToEnitity());

            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, result.Id.ToUpper());
            if (result != null && cache.Save(queryCacheKey, result.ToModel()))
            {
                return result.ToModel();
            }

            return null;
        }

        public async Task<UserModel> Delete(string id)
        {
            var result = await repository.Delete(id);
            return new UserModel { Id = result };
        }
    }
}