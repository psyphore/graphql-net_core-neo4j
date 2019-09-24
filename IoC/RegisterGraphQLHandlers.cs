using GraphQL.Types;
using GraphQL.Validation;
using GraphQLCore;
using GraphQLCore.Unions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Models.GraphQLTypes.Building;
using Models.GraphQLTypes.Person;
using Models.GraphQLTypes.Product;
using Models.GraphQLTypes.Search;
using Models.Types;
using System.Threading.Tasks;

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

            // GraphQL Schema
            services.AddSingleton<ISchema, MainSchema>();

            services.AddGraphQLAuth();
        }

        public static void UseGraphQLAuth(this IApplicationBuilder app)
        {
            var settings = new GraphQLSettings
            {
                Path = "/graphql",
                BuildUserContext = ctx =>
                {
                    var userContext = new GraphQLUserContext
                    {
                        User = ctx.User
                    };

                    return Task.FromResult(userContext);
                },
                EnableMetrics = true
            };

            var rules = app.ApplicationServices.GetServices<IValidationRule>();
            settings.ValidationRules.AddRange(rules);

            app.UseMiddleware<GraphQLMiddleware>(settings);
        }

        public static void AddGraphQLAuth(this IServiceCollection services)
        {
            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //services.TryAddSingleton<IAuthorizationEvaluator, AuthorizationEvaluator>();
            //services.AddTransient<IValidationRule, AuthorizationValidationRule>();

            //services.TryAddSingleton(s =>
            //{
            //    var authSettings = new AuthorizationSettings();

            //    authSettings.AddPolicy("AdminPolicy", _ => _.RequireClaim("role", "Admin"));

            //    return authSettings;
            //});
        }
    }
}