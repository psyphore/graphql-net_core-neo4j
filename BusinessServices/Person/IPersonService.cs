using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Person
{
    public interface IPersonService
    {
        Task<object> Get(string id);
        Task<object> Update(object person);
        Task<object> Delete(string id);
    }
}
