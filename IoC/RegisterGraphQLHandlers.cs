using GraphQLCore.GraphQLTypes.Building;
using GraphQLCore.GraphQLTypes.Person;
using GraphQLCore.GraphQLTypes.Product;
using GraphQLCore.GraphQLTypes.Search;
using GraphQLCore.Unions;
using Microsoft.Extensions.DependencyInjection;
using Models.Types;

namespace IoC
{
    internal static class RegisterGraphQLHandlers
    {
        public static void ConfigureGraphQLServices(this IServiceCollection services)
        {
            // Person
            services.AddSingleton<PersonType>();
            services.AddSingleton<PersonInputType>();
            services.AddSingleton<IGraphQueryMarker, PersonQuery>();
            services.AddSingleton<IGraphMutator, PersonMutation>();

            // Product
            services.AddSingleton<ProductInputType>();
            services.AddSingleton<ProductType>();
            services.AddSingleton<IGraphMutator, ProductMutation>();
            services.AddSingleton<IGraphQueryMarker, ProductQuery>();

            // Building
            services.AddSingleton<BuildingInputType>();
            services.AddSingleton<BuildingType>();
            services.AddSingleton<IGraphMutator, BuildingMutation>();
            services.AddSingleton<IGraphQueryMarker, BuildingQuery>();

            // Search
            services.AddSingleton<SearchInputType>();
            services.AddSingleton<SearchType>();
            services.AddSingleton<IGraphQueryMarker, SearchQuery>();

            // Unions
            services.AddSingleton<Mutations>();
            services.AddSingleton<Queries>();

            // Composites
            services.AddSingleton<CompositeQueries>();
            services.AddSingleton<CompositeMutators>();
        }

    }
}