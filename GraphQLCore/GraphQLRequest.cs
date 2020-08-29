using Newtonsoft.Json.Linq;

namespace GraphQLCore
{
    public class GraphQLRequest
    {
        public string OperationName { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
        public string NamedQuery { get; set; }
    }
}
