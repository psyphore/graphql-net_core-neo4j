using GraphQL.Types;
using net_core_graphql.Helper;
using net_core_graphql.Models;

namespace net_core_graphql.Types
{
    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType(ContextServiceLocator contextServiceLocator)
        {
            Field(x => x.Id);
            Field(x => x.Firstname, true);
            Field(x => x.Lastname, true);
            Field(x => x.BirthPlace);
            Field(x => x.Height);
            Field(x => x.WeightLbs);

            Field<StringGraphType>("birthDate",
                resolve:
                context => context.Source.BirthDate.ToShortDateString());

            Field<ListGraphType<StringGraphType>>("skaterSeasonStats",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => contextServiceLocator.PersonRepository.Get(context.Source.Id), description: "Player's skater stats");
        }
    }
}