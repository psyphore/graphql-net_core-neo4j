using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs.Configuration;
using net_core_graphql.Filters;
using System.Text;

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
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseCors(p => p
                .WithOrigins(new[] { "http://localhost:4201", "http://sipholpt:4201" })
                .WithMethods(new[] { "OPTIONS", "GET", "POST" })
                .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.ConfigureApp();

            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            var ws = new WebSocketOptions();
            ws.AllowedOrigins.Add("http://localhost:4201");
            ws.AllowedOrigins.Add("http://sipholpt:4201");

            app.UseWebSockets(ws);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute().RequireAuthorization();
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
                //endpoints.MapHub<ChatHub>("/chat");   // SignalR
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMemoryCache();

            services.AddSingleton<IAuthorizationPolicyProvider, Auth0PolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, Auth0AuthorizationHandler>();

            services.ConfigureServices(Configuration);

            var auth0conf = Configuration.GetSection("Auth0");
            services.Configure<Auth0>(auth0conf);
            var auth0 = auth0conf.Get<Auth0>();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(jb =>
            {
                /*  // validate against Auth0
                    jb.Authority = $"https://{auth0.Domain}/oauth2/default";
                    jb.Audience = "api://default";
                    jb.RequireHttpsMetadata = false;
                 */

                // validate locally
                jb.RequireHttpsMetadata = false;
                jb.SaveToken = true;
                jb.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(auth0.Secret)),

                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true
                };
            })
                ;

            services.AddHealthChecks()
                .AddCheck("Example", () => HealthCheckResult.Healthy("Example is OK!"), tags: new[] { "example" });

            services.AddGraphQL(o => { o.ExposeExceptions = false; })
                .AddGraphTypes(ServiceLifetime.Scoped);

            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.MaxDepth = 15)
                .AddMvcOptions(o => o.EnableEndpointRouting = false);
        }
    }
}