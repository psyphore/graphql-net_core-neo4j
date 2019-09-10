using System;
using BusinessServices.Person;
using DataAccess;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC
{
    public static class Registration
    {
        public static IConfiguration Configuration { get; }

        public static void RegisterTypes(IServiceCollection services)
        {
            // Frameworks

            // Repositories
            services.AddTransient(o =>
            {
                return new Repository(Configuration["ConnectionString:BoltURL"], Configuration["ConnectionString:Username"], Configuration["ConnectionString:Password"]);
            });
            services.AddTransient<IRepository, Repository>();

            // Business Services
            services.AddTransient<IPersonService, PersonService>();

        }
    }
}
