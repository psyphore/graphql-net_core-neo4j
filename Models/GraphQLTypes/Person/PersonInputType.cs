using GraphQL.Types;

namespace Models.GraphQLTypes.Person
{
    public class PersonInputType : InputObjectGraphType
    {
        public PersonInputType()
        {
            Name = "PersonInput";
            Field<NonNullGraphType<StringGraphType>>("firstname");
            Field<NonNullGraphType<StringGraphType>>("lastname");
            //Field<StringGraphType>("birthPlace");
            //Field<StringGraphType>("height");
            //Field<IntGraphType>("weightLbs");
            //Field<DateGraphType>("birthDate");
        }
    }
}