using BusinessServices.Building;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLCore.BuildingHandlers
{
    /// <summary>
    /// Actions fetch a Building
    /// </summary>
    [ExtendObjectType(Name = "Query")]
    public class BuildingQuery
    {
        private readonly IBuildingService service;

        public BuildingQuery(IBuildingService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Fetch a Building by their Identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BuildingModel> Building(string id) => await service.Get(id);

        /// <summary>
        /// Fetch all Buildings
        /// </summary>
        /// <returns></returns>
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<BuildingModel>> Buildings() => await service.GetAll();

    }
}