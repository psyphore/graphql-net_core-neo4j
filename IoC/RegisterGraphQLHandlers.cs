using GraphQLCore;
using GraphQLCore.BuildingHandlers;
using GraphQLCore.PersonHandlers;
using GraphQLCore.ProductHandlers;
using GraphQLCore.SearchHandlers;
using GraphQLCore.UserHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    internal static class RegisterGraphQLHandlers
    {
        public static IServiceCollection ConfigureGraphQLServices(this IServiceCollection services)
        {
            // Person
            services.AddTransient<PersonQuery>();
            services.AddTransient<PersonMutation>();

            // User
            services.AddTransient<UserQuery>();
            services.AddTransient<UserMutation>();

            // Product
            services.AddTransient<ProductMutation>();
            services.AddTransient<ProductQuery>();

            // Building
            services.AddTransient<BuildingMutation>();
            services.AddTransient<BuildingQuery>();

            // Search
            services.AddTransient<SearchQuery>();

            services
                .AddSingleton<Query>()
                .AddSingleton<Mutation>()
                .AddSingleton<Subscription>()
                ;

            return services;
        }
    }
}