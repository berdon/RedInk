using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Engine.Configuration;
using Engine.Core;
using Engine.Layout;
using Engine.Middleware;
using Engine.Plugin;
using Engine.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Engine
{
    public class Startup
    {
        public static EngineConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddScoped<ILayoutComponentHelper, LayoutComponentHelper>();
            services.AddAuthentication()
                .AddCookieAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddSingleton<IEngine, Core.Engine>();
            services.AddScoped<IDbConnection>(provider =>
                new Npgsql.NpgsqlConnection(Configuration.Database.ConnectionString()));
            services.AddSingleton<IPluginManager, MockPluginManager>();
            services.AddSingleton<ILayoutManager, MockLayoutManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<UserMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IEngine engine, IPluginManager pluginManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMiddleware<UserMiddleware>();

            app.UseMvc(routeBuilder => {
                engine.Initialize(app, routeBuilder);

                foreach (var plugin in pluginManager.Plugins<IRequestPlugin>()) {
                    plugin.Initialize(app);
                }

                foreach (var plugin in pluginManager.Plugins<IMvcPlugin>()) {
                    plugin.Initialize(engine);
                }

                routeBuilder.MapRoute(
                    name: "default",
                    template: "{controller=Index}/{action=Index}"
                );
            });

            app.Run((context) =>
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            });
        }
    }
}
