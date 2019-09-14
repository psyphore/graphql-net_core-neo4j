using BusinessServices.Person;
using GraphQL.Types;
using Models.DTOs;
using Models.GraphQLTypes.Person;

namespace Models.Types
{
    public class PersonMutation : ObjectGraphType
    {
        public PersonMutation(IPersonService service)
        {
            Name = "PersonMutation";
            Description = "Actions to create, update and delete a person";

            Field<PersonType>(
                "Create",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<PersonInputType>> { Name = "person" }),
                resolve: ctx => service.Add(ctx.GetArgument<PersonModel>("person")),
                description: "Create a new person"
                );

            Field<PersonType>(
                "Update",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<PersonInputType>> { Name = "person" }),
                resolve: ctx => service.Update(ctx.GetArgument<PersonModel>("person")),
                description: "Update a person"
                );

            Field<PersonType>(
                "Remove",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
                resolve: ctx => service.Delete(ctx.GetArgument<string>("id")),
                description: "Delete a person"
                );
        }
    }
}