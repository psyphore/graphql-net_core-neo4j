using BusinessServices.Person;
using GraphQL.Types;
using GraphQLCore.Unions;
using Models.DTOs;
using GraphQLCore.GraphQLTypes.Person;

namespace Models.Types
{
    public class PersonMutation : ObjectGraphType, IGraphMutator
    {
        public PersonMutation(IPersonService service)
        {
            Name = "PersonMutation";
            Description = "Actions to create, update and delete a person";

            Field<PersonType>(
                "CreatePerson",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<PersonInputType>> { Name = "person" }),
                resolve: ctx => service.Add(ctx.GetArgument<PersonModel>("person")),
                description: "Create a new person"
                );

            Field<PersonType>(
                "UpdatePerson",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<PersonInputType>> { Name = "person" }),
                resolve: ctx => service.Update(ctx.GetArgument<PersonModel>("person")),
                description: "Update a person"
                );

            Field<PersonType>(
                "RemovePerson",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
                resolve: ctx => service.Delete(ctx.GetArgument<string>("id")),
                description: "Delete a person"
                );
        }
    }
}