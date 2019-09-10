using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using net_core_graphql.Data;
using net_core_graphql.Data.Core;
using net_core_graphql.Data.Interfaces;
using net_core_graphql.Helper;
using net_core_graphql.Types;

namespace net_core_graphql
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseGraphiQl();
            app.UseMvc();

            var ws = new WebSocketOptions();
            ws.AllowedOrigins.Add("*");

            app.UseWebSockets(ws);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
            services.AddSingleton<ContextServiceLocator>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddTransient(o =>
            {
                var connectionString = Configuration["ConnectionString:BoltURL"];
                var username = Configuration["ConnectionString:Username"];
                var password = Configuration["ConnectionString:Password"];

                return new DbContext(connectionString, username, password);
            });
            services.AddTransient<IPersonRepository, PersonRepository>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<PersonQuery>();
            services.AddSingleton<PersonMutation>();
            services.AddSingleton<PersonType>();

            //IoC.Registration.Configuration = Configuration;
            IoC.Registration.RegisterTypes(services);

            var sp = services.BuildServiceProvider();
            using (var mainSchema = new MainSchema(new FuncDependencyResolver(type => sp.GetService(type))))
                services.AddSingleton<ISchema>(mainSchema);
        }
    }
}