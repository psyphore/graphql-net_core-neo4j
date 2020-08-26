using DataAccess.Interfaces;
using Neo4j.Driver;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Building
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly IDictionary<string, string> _mutations;
        private readonly IDictionary<string, string> _queries;
        private readonly IRepository _repository;
        const string LABEL = "building";

        public BuildingRepository(IRepository repository)
        {
            _repository = repository;
            _queries = new BuildingQueries().Queries;
            _mutations = new BuildingQueries().Mutations;
        }

        public async Task<Building> Add(Building building)
        {
            var entity = await _repository.Write<Building>(_mutations["UPDATE_BUILDING"].Trim(), building);
            return entity;
        }

        public async Task<IEnumerable<Building>> All()
        {
            var x = new List<Building>();
            const int First = 9999;
            const int Offset = 0;
            var param = new Dictionary<string, object>()
            {
                {
                    "offset", Offset
                },
                {
                    "first", First
                }
            };
            var query = _queries["GET_BUILDINGS"].Trim();
            var records = await _repository.Read(query, param);
            x.AddRange(records.Select(record => ProcessProps(record, LABEL)));
            return x;
        }

        public async Task<string> Delete(string id)
        {
            var entity = await _repository.Write<Building>(_mutations["DEACTIVATE_BUILDING"].Trim(), new { id });
            return entity.Id;
        }

        public async Task<Building> Get(string id)
        {
            var x = new List<Building>();
            var param = new Dictionary<string, object>
            {
                {
                    "id", id
                }
            };
            var query = _queries["GET_BUILDING"].Trim();
            var entity = await _repository.Read(query, param);
            x.AddRange(entity.Select(e => ProcessProps(e, LABEL)));
            return x.FirstOrDefault();
        }

        public async Task<Building> Update(Building building)
        {
            var entity = await _repository.Write<Building>(_mutations["UPDATE_BUILDING_2"].Trim(), building);
            return entity;
        }

        private Building ProcessProps(IRecord record, string label)
        {
            var props = JsonConvert.SerializeObject(record[label]);
            var person = JsonConvert.DeserializeObject<Building>(props);

            if (person.People == null)
            {
                var lineProps = ((Dictionary<string, object>)record.Values.Values.First()).FirstOrDefault(v => v.Key == "people");
                if (lineProps.Value != null && ((List<object>)lineProps.Value).Any())
                {
                    var lines = ((List<object>)lineProps.Value)
                        .Select(l => JsonConvert.DeserializeObject<Person.Person>(JsonConvert.SerializeObject(l.As<INode>().Properties)));
                    person.People = lines.ToList();
                }
                else
                    person.People = new List<Person.Person>();
            }

            return person;
        }
    }
}