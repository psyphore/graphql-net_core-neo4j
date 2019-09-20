using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQL.Validation;
using GraphQLCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Models.GraphQLTypes.Person;
using Models.Types;
using System.Threading.Tasks;

namespace IoC
{
    internal static class RegisterGraphQLHandlers
    {
        public static void ConfigureGraphQLServices(this IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            // Person
            services.AddSingleton<PersonQuery>();
            services.AddSingleton<PersonMutation>();
            services.AddSingleton<PersonType>();
            services.AddSingleton<PersonInputType>();

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