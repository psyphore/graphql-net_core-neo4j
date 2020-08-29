using DataAccess.Building;
using DataAccess.CacheProvider;
using DataAccess.Person;
using DataAccess.Product;
using DataAccess.Search;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    internal static class RegisterRepositories
    {
        public static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ICacheProvider, InMemoryCache>();
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddTransient<IBuildingRepository, BuildingRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISearchRepository, SearchRepository>();
        }
    }
}