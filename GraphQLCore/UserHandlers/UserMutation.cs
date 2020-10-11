using BusinessServices.User;
using HotChocolate.Types;
using Models.DTOs;
using System.Threading.Tasks;
using HotChocolate.AspNetCore.Authorization;

namespace GraphQLCore.UserHandlers
{
    /// <summary>
    /// Actions to create, update and delete a person
    /// </summary>
    [ExtendObjectType(Name = "Mutation")]
    public class UserMutation
    {
        private readonly IUserService service;

        public UserMutation(IUserService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Create a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<UserModel> CreatePersonAsync(UserModel person) => await service.Add(person);

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<UserModel> UpdatePersonAsync(UserModel person) => await service.Update(person);

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<UserModel> DeletePersonAsync(string id) => await service.Delete(id);
    }
}