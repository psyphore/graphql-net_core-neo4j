using BusinessServices.Search;
using GraphQL.Types;
using GraphQLCore.Unions;
using Models.DTOs;
using GraphQLCore.GraphQLTypes.Building;
using GraphQLCore.GraphQLTypes.Search;

namespace Models.Types
{
    public class SearchQuery : ObjectGraphType, IGraphQueryMarker
    {
        public SearchQuery(ISearchService service)
        {
            Name = "SearchQuery";
            Description = "Search for person or people";

            Field<BuildingType>(
                "Search",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<SearchType>> { Name = "criteria" }),
                resolve: ctx => service.Get(ctx.GetArgument<SearchCriteriaModel>("criteria")),
                description: "Search"
                );

        }
    }
}