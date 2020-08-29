using GraphQL.Types;
using Models.DTOs;
using GraphQLCore.GraphQLTypes.Person;

namespace GraphQLCore.GraphQLTypes.Building
{
    public class BuildingType : ObjectGraphType<BuildingModel>
    {
        public BuildingType()
        {
            Name = "Building";

            Field(x => x.Id, type: typeof(IdGraphType)).Description("The ID of the Building.");
            Field(x => x.Name, false).Description("The Name of the Building");
            Field(x => x.Address, false).Description("The Physical address of the Building");
            Field(x => x.Avatar, true).Description("The Image of the Building");
            Field(x => x.HeadCount, true).Description("The Number of people based in the Building");
            //Field(x => x.People, true);
            Field(x => x.Deactivated, true);

            Field<ListGraphType<PersonType>>("People", resolve: ctx => new ListGraphType<PersonType>());
        }
    }
}