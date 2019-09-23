using GraphQL.Types;
using Models.DTOs;
using Models.GraphQLTypes.Building;
using Models.GraphQLTypes.Product;

namespace Models.GraphQLTypes.Person
{ 
    public class PersonType : ObjectGraphType<PersonModel>
    {
        public PersonType()
        {
            Field(x => x.Id);
            Field(x => x.Firstname, true);
            Field(x => x.Lastname, true);
            Field(x => x.Title, true);
            Field(x => x.Email, true);
            Field(x => x.Mobile, true);
            Field(x => x.Bio, true);
            Field(x => x.Avatar, true);
            Field(x => x.KnownAs, true);
            Field(x => x.Deactivated, true);

            Field<PersonType>("Manager", resolve: ctx => new PersonType());
            Field<ListGraphType<PersonType>>("Team", resolve: ctx => new ListGraphType<PersonType>());
            Field<ListGraphType<PersonType>>("Line", resolve: ctx => new ListGraphType<PersonType>());
            Field<ListGraphType<BuildingType>>("Buildings", resolve: ctx => new ListGraphType<BuildingType>());
            Field<ListGraphType<ProductType>>("Products", resolve: ctx => new ListGraphType<ProductType>());

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