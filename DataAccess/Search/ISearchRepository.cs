using System.Threading.Tasks;

namespace DataAccess.Search
{
    public interface ISearchRepository
    {
        Task<Search> Get(string query, int first = 9999, int offset = 0);
    }
}