using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using GraphQLCore;
using Microsoft.Extensions.DependencyInjection;
using Models.GraphQLTypes.Person;
using Models.Types;

namespace IoC
{
    internal class RegisterGraphQLHandlers
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDocumentWriter, DocumentWriter>();

            // Person
            services.AddSingleton<PersonQuery>();
            services.AddSingleton<PersonMutation>();
            services.AddSingleton<PersonType>();

            // GraphQL Schema
            services.AddSingleton<ISchema, MainSchema>();

            //var sp = services.BuildServiceProvider();
            //using (var mainSchema = new MainSchema(new FuncDependencyResolver(type => sp.GetService(type))))
            //    services.AddSingleton<ISchema>(mainSchema);
        }
    }
}