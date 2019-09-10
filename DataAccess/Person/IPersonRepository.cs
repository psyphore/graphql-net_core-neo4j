using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Person
{
    public interface IPersonRepository
    {
        Task<Person> Get(string id);

        Task<IEnumerable<Person>> All();

        Task<Person> Add(Person person);

        Task<Person> Update(Person person);

        Task<string> Delete(string id);
    }
}