using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServices.Building
{
    public interface IBuildingService
    {
        Task<BuildingModel> Add(BuildingModel building);

        Task<BuildingModel> Delete(string id);

        Task<BuildingModel> Get(string id);

        Task<IEnumerable<BuildingModel>> GetAll();

        Task<BuildingModel> Update(BuildingModel building);
    }
}