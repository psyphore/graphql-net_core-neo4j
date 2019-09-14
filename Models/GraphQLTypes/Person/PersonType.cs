using GraphQL.Types;
using Models.DTOs;

namespace Models.GraphQLTypes.Person
{ 
    public class PersonType : ObjectGraphType<PersonModel>
    {
        public PersonType() //ContextServiceLocator contextServiceLocator
        {
            Field(x => x.Id);
            Field(x => x.Firstname, true);
            Field(x => x.Lastname, true);
            // Field(x => x.Manager);
            Field(x => x.Team);
            Field(x => x.Line);
            Field(x => x.Buildings);

            //Field<StringGraphType>("birthDate",
            //    resolve:
            //    context => context.Source.BirthDate.ToShortDateString());

            //Field<ListGraphType<StringGraphType>>("GetPerson",
            //    arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
            //    resolve: context => contextServiceLocator.PersonService.Get(context.Source.Id),
            //    description: "Player's skater stats");
        }
    }
}