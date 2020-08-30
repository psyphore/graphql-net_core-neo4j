using System.Threading.Tasks;
using BusinessServices.Search;
using HotChocolate.Types;
using Models.DTOs;

namespace GraphQLCore.SearchHandlers
{
    /// <summary>
    /// Search for person or people
    /// </summary>
    [ExtendObjectType(Name = "Query")]
    public class SearchQuery
    {
        private readonly ISearchService service;

        public SearchQuery(ISearchService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public async Task<SearchModel> SearchAsync(SearchCriteriaModel criteria) => await service.Get(criteria);
    }
}