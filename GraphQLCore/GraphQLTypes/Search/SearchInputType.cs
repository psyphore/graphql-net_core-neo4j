using GraphQL.Types;
using Models.DTOs;

namespace GraphQLCore.GraphQLTypes.Search
{
    public class SearchInputType : InputObjectGraphType<SearchCriteriaModel>
    {
        public SearchInputType()
        {
            Name = "SearchInput";

            Field(x => x.Query);
            Field(x => x.First, true).DefaultValue(100);
            Field(x => x.Offset, true).DefaultValue(0);
        }
    }
}