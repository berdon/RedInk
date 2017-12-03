using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Engine.Core
{
    public class Engine : IEngine, IInternalEngine
    {
        private IRouteBuilder _mvcRouteBuilder;
        private IApplicationBuilder _app;
        private IRouteBuilder _internalRouteBuilder;
        private readonly IServiceProvider _provider;

        public Engine(IAdmin admin)
        {
            this.Admin = admin;

        }
        public IAdmin Admin { get; private set; }

        public void Initialize(IApplicationBuilder app, IRouteBuilder builder)
        {
            Admin = ActivatorUtilities.CreateInstance<Admin>(_provider, this);

            _app = app;
            _mvcRouteBuilder = builder;

            _internalRouteBuilder = new RouteBuilder(app);
        }

        public IEngine MapMvcRoute(string name, string template)
        {
            _mvcRouteBuilder.MapRoute(name, template);
            return this;
        }

        public IEngine MapRoute(string route, Func<RequestDelegate, RequestDelegate> handler)
        {
            _app.Use(handler);
            return this;
        }

        public IEngine MapRoute(string name, string template, string viewComponent, bool requireAuthenticated)
        {
            _mvcRouteBuilder.MapRoute(
                name: name,
                template: template,
                defaults: new { controller = "Plugin", action = "Index" },
                constraints: null,
                dataTokens: new
                {
                    ViewComponent = viewComponent,
                    RequireAuthenticated = requireAuthenticated
                }
            );
            return this;
        }
    }
}