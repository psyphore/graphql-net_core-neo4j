using BusinessServices.Building.Extensions;
using DataAccess.Building;
using DataAccess.CacheProvider;
using Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessServices.Building
{
    public class BuildingService : IBuildingService
    {
        private readonly IBuildingRepository repository;
        private readonly ICacheProvider _cache;
        private const string CACHE_KEY_PREFIX = "BUILDING_QUERY";

        public BuildingService(IBuildingRepository repository, ICacheProvider cache)
        {
            this.repository = repository;
            this._cache = cache;
        }

        public async Task<BuildingModel> Get(string id)
        {
            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, id.ToUpper());
            var found = _cache.Fetch<BuildingModel>(queryCacheKey);
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

        public async Task<BuildingModel> Update(BuildingModel building)
        {
            var result = await repository.Update(building.ToEnitity());

            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, result.Id.ToUpper());
            if (result != null && _cache.Save(queryCacheKey, result.ToModel()))
            {
                return result.ToModel();
            }

            return null;
        }

        public async Task<IEnumerable<BuildingModel>> GetAll()
        {
            var result = await repository.All();
            return result.Select(r => r.ToModel());
        }

        public async Task<BuildingModel> Create(BuildingModel building)
        {
            var result = await repository.Add(building.ToEnitity());
            return result.ToModel();
        }

        public async Task<BuildingModel> Add(BuildingModel building)
        {
            var result = await repository.Add(building.ToEnitity());

            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, result.Id.ToUpper());
            if (result != null && _cache.Save(queryCacheKey, result.ToModel()))
            {
                return result.ToModel();
            }

            return null;
        }

        public async Task<BuildingModel> Delete(string id)
        {
            var result = await repository.Delete(id);
            return new BuildingModel { Id = id };
        }
    }
}