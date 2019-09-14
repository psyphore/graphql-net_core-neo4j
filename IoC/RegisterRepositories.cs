using DataAccess;
using DataAccess.CacheProvider;
using DataAccess.Interfaces;
using DataAccess.Person;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    internal class RegisterRepositories
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddSingleton<ICacheProvider, InMemoryCache>();
            services.AddTransient<IPersonRepository, PersonRepository>();
        }
    }
}