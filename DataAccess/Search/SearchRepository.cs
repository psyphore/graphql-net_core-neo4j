using DataAccess.Interfaces;
using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Search
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IDictionary<string, string> _queries;
        private readonly IRepository _repository;
        private const string LABEL = "person";

        public SearchRepository(IRepository repository)
        {
            _repository = repository;
            _queries = new SearchQueries().Queries;
        }

        public async Task<Search> Get(string query, int first = 9999, int offset = 0)
        {
            var x = new List<Search>();
            var param = new Dictionary<string, object>()
            {
                { "query", query },
                { "first", first },
                { "offset", offset }
            };
            var statement = _queries["ADVANCED_SEARCH"].Trim();
            var records = await _repository.Read(query, param);

            x.AddRange(records.Select(record => ProcessProps(record, LABEL)));

            return x.FirstOrDefault();
        }

        private Search ProcessProps(IRecord record, string label)
        {
            var props = JsonConvert.SerializeObject(record[label]);
            var search = JsonConvert.DeserializeObject<Search>(props);

            return search;
        }
    }
}