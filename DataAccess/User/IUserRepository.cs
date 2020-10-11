using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.User
{
    public interface IUserRepository
    {
        Task<User> Get(string id);

        Task<IEnumerable<User>> All();

        Task<User> Add(User user);

        Task<User> Update(User user);

        Task<string> Delete(string id);
    }
}