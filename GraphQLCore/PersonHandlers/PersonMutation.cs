using BusinessServices.Person;
using HotChocolate.Types;
using Models.DTOs;
using System.Threading.Tasks;
using HotChocolate.AspNetCore.Authorization;

namespace GraphQLCore.PersonHandlers
{
    /// <summary>
    /// Actions to create, update and delete a person
    /// </summary>
    [ExtendObjectType(Name = "Mutation")]
    public class PersonMutation
    {
        private readonly IPersonService service;

        public PersonMutation(IPersonService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Create a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<PersonModel> CreatePersonAsync(PersonModel person) => await service.Add(person);

        /// <summary>
        /// Update a person
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<PersonModel> UpdatePersonAsync(PersonModel person) => await service.Update(person);

        /// <summary>
        /// Delete a person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<PersonModel> DeletePersonAsync(string id) => await service.Delete(id);
    }
}