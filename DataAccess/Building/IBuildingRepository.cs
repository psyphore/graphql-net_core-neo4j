using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Building
{
    public interface IBuildingRepository
    {
        Task<Building> Get(string id);

        Task<IEnumerable<Building>> All();

        Task<Building> Add(Building building);

        Task<Building> Update(Building building);

        Task<string> Delete(string id);
    }
}