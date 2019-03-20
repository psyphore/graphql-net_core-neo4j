using GraphQL.Types;
using net_core_graphql.Helper;

namespace net_core_graphql.Types
{
    public class PersonQuery : ObjectGraphType
    {
        public PersonQuery(ContextServiceLocator context)
        {
            Field<PersonType>(
                "Person",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
                resolve: ctx => context.PersonRepository.Get(ctx.GetArgument<string>("id"))
                );

            Field<ListGraphType<PersonType>>(
                "People",
                resolve: ctx => context.PersonRepository.All());
        }
    }
}