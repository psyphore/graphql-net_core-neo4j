using BusinessServices.Building;
using GraphQL.Types;
using GraphQLCore.Unions;
using Models.DTOs;
using GraphQLCore.GraphQLTypes.Building;
using GraphQLCore.GraphQLTypes.Person;

namespace Models.Types
{
    public class BuildingMutation : ObjectGraphType, IGraphMutator
    {
        public BuildingMutation(IBuildingService service)
        {
            Name = "BuildingMutation";
            Description = "Actions to create, update and delete a Building";

            Field<BuildingType>(
                "CreateBuilding",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<BuildingInputType>> { Name = "building" }),
                resolve: ctx => service.Add(ctx.GetArgument<BuildingModel>("building")),
                description: "Create a new Building"
                );

            Field<BuildingType>(
                "UpdateBuilding",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<BuildingInputType>> { Name = "building" }),
                resolve: ctx => service.Update(ctx.GetArgument<BuildingModel>("building")),
                description: "Update a Building"
                );

            Field<BuildingType>(
                "RemoveBuilding",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id" }),
                resolve: ctx => service.Delete(ctx.GetArgument<string>("id")),
                description: "Delete a Building"
                );
        }
    }
}