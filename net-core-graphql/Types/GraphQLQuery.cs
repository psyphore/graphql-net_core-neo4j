using Newtonsoft.Json.Linq;

namespace net_core_graphql.Types
{
    public class GraphQLQuery
    {
        public string NamedQuery { get; set; }
        public string OperationName { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; } //https://github.com/graphql-dotnet/graphql-dotnet/issues/389
    }
}