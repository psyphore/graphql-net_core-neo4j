using GraphQL;
using GraphQL.Types;

namespace net_core_graphql.Types
{
    public class MainSchema : Schema
    {
        public MainSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<PersonQuery>();
            Mutation = resolver.Resolve<PersonMutation>();
        }
    }
}