using GraphiQl;
using IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            app.UseCors(p => p
                .WithOrigins(new[] { "http://localhost:4201", "http://sipholpt:4201" })
                .WithMethods(new[] { "OPTIONS", "GET", "POST" })
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseGraphiQl();

            var ws = new WebSocketOptions();
            ws.AllowedOrigins.Add("*");

            app.UseWebSockets(ws);
            app.UseMvc();

            Registration.ConfigureApp(app);

            app.UseDefaultFiles();
            app.UseStaticFiles();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMemoryCache();

            Registration.RegisterTypes(services, Configuration);

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}