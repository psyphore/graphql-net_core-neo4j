using GraphQL;
using GraphQL.Types;
using Models.Types;

namespace GraphQLCore
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