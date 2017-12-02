using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Engine.Configuration;
using Engine.Core;
using Engine.Plugin;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            services.AddMvc();

            services.AddSingleton<IEngine, Core.Engine>();
            services.AddTransient<IDbConnection>(provider =>
                new Npgsql.NpgsqlConnection(Configuration.Database.ConnectionString()));
            services.AddSingleton<IPluginManager, MockPluginManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IEngine engine, IPluginManager pluginManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routeBuilder => {
                engine.Initialize(app, routeBuilder);

                foreach (var plugin in pluginManager.Plugins<IRequestPlugin>()) {
                    plugin.Initialize(app);
                }

                foreach (var plugin in pluginManager.Plugins<IMvcPlugin>()) {
                    plugin.Initialize(routeBuilder);
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
