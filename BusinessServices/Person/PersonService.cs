using DataAccess.Interfaces;
using DataAccess.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices.Person
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository repository;

        public PersonService(IPersonRepository repository)
        {
            this.repository = repository;
        }

        public async Task<object> Delete(string id)
        {
            var result = await repository.Delete(id);
            return result;
        }

        public async Task<object> Get(string id)
        {
            var result = await repository.Get(id);
            return result;
        }

        public async Task<object> Update(object person)
        {
            var result = await repository.Update((DataAccess.Person.Person)person);
            return result;
        }

        public async Task<IEnumerable<object>> GetAll()
        {
            var result = await repository.All();
            return result;
        }

        public async Task<object> Create(object person)
        {
            var result = await repository.Add((DataAccess.Person.Person)person);
            return result;
        }
    }
}
