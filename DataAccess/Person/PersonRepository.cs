using DataAccess.Interfaces;
using DataAccess.Serializer.Converters;
using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Person
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IDictionary<string, string> _mutations;
        private readonly IDictionary<string, string> _queries;
        private readonly IRepository _repository;

        public PersonRepository(IRepository repository)
        {
            _repository = repository;
            _queries = new PersonQueries().Queries;
            _mutations = new PersonQueries().Mutations;
        }

        public async Task<Person> Add(Person person)
        {
            var entity = await _repository.Write<Person>(_mutations["UPDATE_PERSON"].Trim(), person);
            return entity;
        }

        public async Task<IEnumerable<Person>> All()
        {
            var x = new List<Person>();
            const int First = 9999;
            const int Offset = 0;
            const string LABEL = "person";
            var param = new Dictionary<string, object>()
            {
                //{
                //    "pages", ParameterSerializer.ToDictionary(new List<NodePaging> { new NodePaging(First, Offset) })
                //},
                {
                    "offset", Offset
                },
                {
                    "first", First
                }
            };
            var query = _queries["GET_PEOPLE"].Trim();
            var records = await _repository.Read(query, param);

            x.AddRange(records.Select(record => ProcessProps(record, LABEL)));

            return x;
        }

        public async Task<string> Delete(string id)
        {
            var entity = await _repository.Write<Person>(_mutations["DEACTIVATE_PERSON"].Trim(), new { id });
            return entity.Id;
        }

        public async Task<Person> Get(string id)
        {
            var entity = await _repository.Read<Person>(_queries["GET_PERSON"].Trim(), new { id });
            return entity;
        }

        public async Task<Person> Update(Person person)
        {
            var entity = await _repository.Write<Person>(_mutations["UPDATE_PERSON_2"].Trim(), person);
            return entity;
        }

        private Person ProcessProps(IRecord record, string label)
        {
            var props = JsonConvert.SerializeObject(record[label]);
            var person = JsonConvert.DeserializeObject<Person>(props);

            if(person.Manager == null)
            {
                var managerProps = ((Dictionary<string, object>)record.Values.Values.First()).FirstOrDefault(v => v.Key == "manager");
                person.Manager = managerProps.Value != null ? JsonConvert.DeserializeObject<Person>(JsonConvert.SerializeObject(managerProps.Value.As<INode>().Properties)) : null;
            }

            if(person.Line == null)
            {
                var lineProps = ((Dictionary<string, object>)record.Values.Values.First()).FirstOrDefault(v => v.Key == "line");
                if(lineProps.Value != null && ((List<object>)lineProps.Value).Any())
                {
                    var lines = ((List<object>)lineProps.Value)
                        .Select(l => JsonConvert.DeserializeObject<Person>(JsonConvert.SerializeObject(l.As<INode>().Properties)));
                    person.Line = lines.ToList();
                }
                else
                    person.Line = new List<Person>();
            }

            if(person.Team == null)
            {
                var teamProps = ((Dictionary<string, object>)record.Values.Values.First()).FirstOrDefault(v => v.Key == "team");
                if (teamProps.Value != null && ((List<object>)teamProps.Value).Any())
                {
                    var team = ((List<object>)teamProps.Value)
                        .Select(l => JsonConvert.DeserializeObject<Person>(JsonConvert.SerializeObject(l.As<INode>().Properties)));
                    person.Team = team.ToList();
                }
                else
                    person.Team = new List<Person>();
            }

            return person;
        }
    }
}