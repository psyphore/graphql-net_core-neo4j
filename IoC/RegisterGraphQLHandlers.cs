using GraphQLCore;
using GraphQLCore.BuildingHandlers;
using GraphQLCore.PersonHandlers;
using GraphQLCore.ProductHandlers;
using GraphQLCore.SearchHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    internal static class RegisterGraphQLHandlers
    {
        public static IServiceCollection ConfigureGraphQLServices(this IServiceCollection services)
        {
            // Person
            services.AddSingleton<PersonQuery>();
            services.AddSingleton<PersonMutation>();

            // Product
            services.AddSingleton<ProductMutation>();
            services.AddSingleton<ProductQuery>();

            // Building
            services.AddSingleton<BuildingMutation>();
            services.AddSingleton<BuildingQuery>();

            // Search
            services.AddSingleton<SearchQuery>();

            services.AddSingleton<Query>()
                .AddSingleton<Mutation>()
                .AddSingleton<Subscription>();

            return services;
        }
    }
}