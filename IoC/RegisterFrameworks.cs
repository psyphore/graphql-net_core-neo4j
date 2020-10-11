using DataAccess;
using DataAccess.Interfaces;
using GraphQLCore;
using GraphQLCore.BuildingHandlers;
using GraphQLCore.PersonHandlers;
using GraphQLCore.ProductHandlers;
using GraphQLCore.SearchHandlers;
using GraphQLCore.UserHandlers;
using HotChocolate;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Configuration;
using Models.DTOs.Configuration;
using Neo4j.Driver;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IoC
{
    internal static class RegisterFrameworks
    {
        public static IServiceCollection ConfigureFrameworks(this IServiceCollection services, IConfiguration configuration)
        {
            var auth0conf = configuration.GetSection("Auth0");
            services.Configure<Auth0>(auth0conf);

            var auth0 = auth0conf.Get<Auth0>();
            services.AddSingleton(auth0);

            var dbconf = configuration.GetSection("ConnectionStrings");
            services.Configure<Connection>(dbconf);

            var neo4j = dbconf.Get<Connection>();
            services
                .AddSingleton(neo4j)
                .AddTransient(d => GraphDatabase.Driver(
                neo4j.BoltURL,
                AuthTokens.Basic(neo4j.Username, neo4j.Password),
                o => o.WithEncryptionLevel(EncryptionLevel.None)));

            services
                .AddTransient<IRepository, Repository>()
                .AddDataLoaderRegistry()
                .AddInMemorySubscriptions()
                .AddGraphQL(sp =>
                        SchemaBuilder.New()
                            .AddServices(sp)
                            .AddQueryType<GraphQLCore.Query>(d => d.Name("Query"))
                            .AddType<SearchQuery>()
                            .AddType<BuildingQuery>()
                            .AddType<ProductQuery>()
                            .AddType<PersonQuery>()
                            .AddType<UserQuery>()
                            .AddMutationType<Mutation>(d => d.Name("Mutation"))
                            .AddType<BuildingMutation>()
                            .AddType<ProductMutation>()
                            .AddType<PersonMutation>()
                            .AddType<UserMutation>()
                            .AddSubscriptionType<Subscription>(d => d.Name("Subscription"))
                            .AddType<ProductSubscription>()
                            .AddAuthorizeDirectiveType()
                            .BindClrType<string, StringType>()
                            .BindClrType<Guid, IdType>()
                            .Create(),
                    new QueryExecutionOptions
                    {
                        ForceSerialExecution = true,
                        IncludeExceptionDetails = true
                    }
                );


            services.AddQueryRequestInterceptor(async (context, builder, ct) =>
            {
                if (context.User.Identity.IsAuthenticated)
                {
                    var personId = Guid.Parse(context.User.FindFirst(WellKnownClaimTypes.UserId).Value);

                    builder.AddProperty("currentPersonId", personId);
                    builder.AddProperty("currentUserEmail", context.User.FindFirst(ClaimTypes.Email).Value);

                    //IPersonRepository personRepository = context.RequestServices.GetRequiredService<IPersonRepository>();
                    //await personRepository..UpdateLastSeenAsync(personId, DateTime.UtcNow, ct);
                    await Task.FromResult(true);
                }
            });

            return services;
        }
    }
}