using DataAccess;
using DataAccess.Building;
using DataAccess.CacheProvider;
using DataAccess.Interfaces;
using DataAccess.Person;
using DataAccess.Product;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    internal static class RegisterRepositories
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddTransient<IRepository, Repository>();
            services.AddSingleton<ICacheProvider, InMemoryCache>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IBuildingRepository, BuildingRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }
    }
}