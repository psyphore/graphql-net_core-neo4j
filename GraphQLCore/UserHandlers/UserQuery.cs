using BusinessServices.User;
using HotChocolate;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GraphQLCore.UserHandlers
{
    /// <summary>
    /// Actions for interacting with a person
    /// </summary>
    [ExtendObjectType(Name = "Query")]
    public class UserQuery
    {
        private readonly IUserService service;

        public UserQuery(IUserService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets a user.
        /// </summary>
        public async Task<UserModel> User([GlobalState] string id) => await service.Get(id);

        /// <summary>
        /// Gets All Users.
        /// </summary>
        /// <returns></returns>
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public async Task<IEnumerable<UserModel>> Users() => await service.GetAll();
    }
}