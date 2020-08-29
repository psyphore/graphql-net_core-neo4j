using GraphQL.Types;

namespace GraphQLCore.GraphQLTypes.Person
{
    public class PersonInputType : InputObjectGraphType
    {
        public PersonInputType()
        {
            Name = "PersonInput";
            Field<NonNullGraphType<StringGraphType>>("firstname");
            Field<NonNullGraphType<StringGraphType>>("lastname");
            Field<StringGraphType>("title");
            Field<StringGraphType>("email");
            Field<StringGraphType>("mobile");
            Field<StringGraphType>("bio");
            Field<StringGraphType>("knownAs");
        }
    }
}