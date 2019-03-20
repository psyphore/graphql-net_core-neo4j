using net_core_graphql.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace net_core_graphql.Data.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> Get(string id);

        Task<IEnumerable<Person>> All();

        Task<Person> Add(Person person);
    }
}