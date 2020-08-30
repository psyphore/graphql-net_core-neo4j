using BusinessServices.Person;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLCore.PersonHandlers
{
    /// <summary>
    /// Actions for interacting with a person
    /// </summary>
    [ExtendObjectType(Name = "Query")]
    public class PersonQuery
    {
        private readonly IPersonService service;

        public PersonQuery(IPersonService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets a user.
        /// </summary>
        public async Task<PersonModel> Person([GlobalState] string id) => await service.Get(id);

        /// <summary>
        /// Gets All Users.
        /// </summary>
        /// <returns></returns>
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<PersonModel>> People() => await service.GetAll();
    }
}