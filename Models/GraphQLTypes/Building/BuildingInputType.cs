using GraphQL.Types;

namespace Models.GraphQLTypes.Person
{
    public class BuildingInputType : InputObjectGraphType
    {
        public BuildingInputType()
        {
            Name = "BuildingInput";
            Field<NonNullGraphType<StringGraphType>>("name");
            Field<NonNullGraphType<StringGraphType>>("address");
        }
    }
}