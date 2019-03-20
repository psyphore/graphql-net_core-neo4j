using GraphQL.Types;
using net_core_graphql.Helper;
using net_core_graphql.Models;

namespace net_core_graphql.Types
{
    public class PersonMutation : ObjectGraphType
    {
        public PersonMutation(ContextServiceLocator context)
        {
            Name = "CreatePersonMutation";

            Field<PersonType>(
                "Person",
                arguments: new QueryArguments(new QueryArgument<PersonInputType> { Name = "person" }),
                resolve: ctx =>
                {
                    var arg = ctx.GetArgument<Person>("person");
                    return context.PersonRepository.Add(arg);
                }
                );
        }
    }
}