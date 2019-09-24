using BusinessServices.Building.Extensions;
using BusinessServices.Search.Extensions;
using DataAccess.CacheProvider;
using DataAccess.Search;
using Models.DTOs;
using System.Threading.Tasks;

namespace BusinessServices.Search
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository repository;
        private readonly ICacheProvider _cache;
        private const string CACHE_KEY_PREFIX = "SEARCH_QUERY";

        public SearchService(ISearchRepository repository, ICacheProvider cache)
        {
            this.repository = repository;
            this._cache = cache;
        }

        public async Task<SearchModel> Get(SearchCriteriaModel model)
        {
            var queryCacheKey = string.Format("{0}_{1}", CACHE_KEY_PREFIX, model.Query.ToUpper());
            var found = _cache.Fetch<SearchModel>(queryCacheKey);
            if (found != null)
            {
                return found;
            }

            var results = await repository.Get(model.Query, model.First, model.Offset);
            if (results != null && _cache.Save(queryCacheKey, results))
            {
                return results.ToModel();
            }

            return null;
        }
    }
}