using BusinessServices.Person;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    internal static class RegisterBusinessServices
    {
        public static void RegisterTypes(IServiceCollection services)
        {
            services.AddTransient<IPersonService, PersonService>();
        }
    }
}