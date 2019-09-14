using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServices.Person
{
    public interface IPersonService
    {
        Task<PersonModel> Add(PersonModel person);

        Task<PersonModel> Delete(string id);

        Task<PersonModel> Get(string id);

        Task<IEnumerable<PersonModel>> GetAll();

        Task<PersonModel> Update(PersonModel person);
    }
}