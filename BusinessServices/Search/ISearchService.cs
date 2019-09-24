using Models.DTOs;
using System.Threading.Tasks;

namespace BusinessServices.Search
{
    public interface ISearchService
    {
        Task<SearchModel> Get(SearchCriteriaModel model);
    }
}