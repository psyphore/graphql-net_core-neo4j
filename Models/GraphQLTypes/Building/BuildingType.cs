using GraphQL.Types;
using Models.DTOs;
using Models.GraphQLTypes.Person;
using System.Collections.Generic;

namespace Models.GraphQLTypes.Building
{ 
    public class BuildingType : ObjectGraphType<BuildingModel>
    {
        public BuildingType()
        {
            Field(x => x.Id);
            Field(x => x.Name, true);
            Field(x => x.Address, true);
            Field(x => x.Avatar, true);
            Field(x => x.HeadCount, true);
            // Field(x => x.People, true);
            Field(x => x.Deactivated, true);

            Field<ListGraphType<PersonType>>("People", resolve: ctx => new ListGraphType<PersonType>());

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