using BusinessServices.Building;
using BusinessServices.Person;
using BusinessServices.Product;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    internal static class RegisterBusinessServices
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IPersonService, PersonService>();
            services.AddTransient<IBuildingService, BuildingService>();
            services.AddTransient<IProductService, ProductService>();
        }
    }
}