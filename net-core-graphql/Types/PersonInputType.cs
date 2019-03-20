using GraphQL.Types;

namespace net_core_graphql.Types
{
    public class PersonInputType : InputObjectGraphType
    {
        public PersonInputType()
        {
            Name = "PersonInput";
            Field<NonNullGraphType<StringGraphType>>("firstname");
            Field<NonNullGraphType<StringGraphType>>("lastname");
            Field<StringGraphType>("birthPlace");
            Field<StringGraphType>("height");
            Field<IntGraphType>("weightLbs");
            Field<DateGraphType>("birthDate");
        }
    }
}