using BusinessServices.Building;
using GraphQL.Types;
using GraphQLCore.Unions;
using Models.GraphQLTypes.Building;

namespace Models.Types
{
    public class BuildingQuery : ObjectGraphType, IGraphQueryMarker
    {
        public BuildingQuery(IBuildingService service)
        {
            Name = "BuildingQuery";
            Description = "Actions fetch a Building";

            Field<BuildingType>(
                "Building",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
                resolve: ctx => service.Get(ctx.GetArgument<string>("id")),
                description: "Fetch a Building by their Identifier"
                );

            Field<ListGraphType<BuildingType>>(
                "Buildings",
                resolve: ctx => service.GetAll(),
                description: "Fetch all Buildings"
                );
        }
    }
}