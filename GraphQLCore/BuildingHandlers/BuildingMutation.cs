using System.Threading.Tasks;
using BusinessServices.Building;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Types;
using Models.DTOs;

namespace GraphQLCore.BuildingHandlers
{
    /// <summary>
    /// Actions to create, update and delete a Building
    /// </summary>
    [ExtendObjectType(Name = "Query")]
    public class BuildingMutation
    {
        private readonly IBuildingService service;

        public BuildingMutation(IBuildingService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Create a new Building
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<BuildingModel> CreateBuilding(BuildingModel model) => await service.Add(model);

        /// <summary>
        /// Update a Building
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<BuildingModel> UpdateBuilding(BuildingModel model) => await service.Update(model);

        /// <summary>
        /// Delete a Building
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<BuildingModel> DeleteBuilding(string id) => await service.Delete(id);
    }
}