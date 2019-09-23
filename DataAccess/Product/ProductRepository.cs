using DataAccess.Interfaces;
using Neo4j.Driver.V1;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Product
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDictionary<string, string> _mutations;
        private readonly IDictionary<string, string> _queries;
        private readonly IRepository _repository;
        private const string LABEL = "product";

        public ProductRepository(IRepository repository)
        {
            _repository = repository;
            _queries = new ProductQueries().Queries;
            _mutations = new ProductQueries().Mutations;
        }

        public async Task<Product> Add(Product person)
        {
            var entity = await _repository.Write<Product>(_mutations["UPDATE_PRODUCT"].Trim(), person);
            return entity;
        }

        public async Task<IEnumerable<Product>> All()
        {
            var x = new List<Product>();
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
            var query = _queries["GET_PRODUCTS"].Trim();
            var records = await _repository.Read(query, param);

            x.AddRange(records.Select(record => ProcessProps(record, LABEL)));

            return x;
        }

        public async Task<string> Delete(string id)
        {
            var entity = await _repository.Write<Product>(_mutations["DEACTIVATE_PRODUCT"].Trim(), new Dictionary<string, object>()
            {
                {
                    "id", id
                }
            });
            return entity.Id;
        }

        public async Task<Product> Get(string id)
        {
            var x = new List<Product>();
            var param = new Dictionary<string, object>()
            {
                {
                    "id", id
                }
            };
            var query = _queries["GET_PRODUCT"].Trim();
            var records = await _repository.Read(query, param);

            x.AddRange(records.Select(record => ProcessProps(record, LABEL)));

            return x.FirstOrDefault();
        }

        public async Task<Product> Update(Product person)
        {
            var entity = await _repository.Write<Product>(_mutations["UPDATE_PRODUCT_2"].Trim(), person);
            return entity;
        }

        private Product ProcessProps(IRecord record, string label)
        {
            var props = JsonConvert.SerializeObject(record[label]);
            var product = JsonConvert.DeserializeObject<Product>(props);

            if (product.Owner == null)
            {
                var managerProps = ((Dictionary<string, object>)record.Values.Values.First()).FirstOrDefault(v => v.Key == "owner");
                product.Owner = managerProps.Value != null ?
                    JsonConvert.DeserializeObject<Person.Person>(JsonConvert.SerializeObject(managerProps.Value.As<INode>().Properties)):
                    null;
            }

            if (product.Champions == null)
            {
                var lineProps = ((Dictionary<string, object>)record.Values.Values.First()).FirstOrDefault(v => v.Key == "champions");
                if (lineProps.Value != null && ((List<object>)lineProps.Value).Any())
                {
                    var lines = ((List<object>)lineProps.Value)
                        .Select(l => JsonConvert.DeserializeObject<Person.Person>(JsonConvert.SerializeObject(l.As<INode>().Properties)));

                    product.Champions = lines.ToList();
                }
                else
                    product.Champions = new List<Person.Person>();
            }

            return product;
        }
    }
}