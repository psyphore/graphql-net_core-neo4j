using GraphiQl;
using IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            app.ConfigureApp();

            app.UseWebSockets(ws);
            app.UseMvc();

            app.UseDefaultFiles();
            app.UseStaticFiles();
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
            }).AddJwtBearer(jb =>
            {
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
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}