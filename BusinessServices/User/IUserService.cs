using Models.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessServices.User
{
    public interface IUserService
    {
        Task<UserModel> Add(UserModel person);

        Task<UserModel> Delete(string id);

        Task<UserModel> Get(string id);

        Task<IEnumerable<UserModel>> GetAll();

        Task<UserModel> Update(UserModel person);
    }
}