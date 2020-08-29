using GraphQL.Server.Ui.Playground;
using IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.ConfigureApp();
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions()); // /ui/playground
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // , IHostEnvironment env
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMemoryCache();
            services.ConfigureServices(Configuration);
            services.AddLogging(builder => builder.AddConsole());
            services.AddHttpContextAccessor();
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }
    }
}