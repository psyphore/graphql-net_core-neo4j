using BusinessServices.Person;
using GraphQL.Types;
using GraphQLCore.Unions;
using GraphQLCore.GraphQLTypes.Person;

namespace Models.Types
{
    public class PersonQuery : ObjectGraphType, IGraphQueryMarker
    {
        public PersonQuery(IPersonService service)
        {
            Name = "PersonQuery";
            Description = "Actions fetch a person";

            Field<PersonType>(
                "Person",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
                resolve: ctx => service.Get(ctx.GetArgument<string>("id")),
                description: "Fetch a person by their Identifier"
                );

            Field<ListGraphType<PersonType>>(
                "People",
                resolve: ctx => service.GetAll(),
                description: "Fetch all people"
                );
        }
    }
}