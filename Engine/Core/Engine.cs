using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Engine.Core
{
    public class Engine : IEngine
    {
        private IRouteBuilder _builder;
        private IApplicationBuilder _app;

        public void Initialize(IApplicationBuilder app, IRouteBuilder builder)
        {
            this._app = app;
            this._builder = builder;
        }

        public IEngine MapRoute(string route, Func<RequestDelegate, RequestDelegate> handler)
        {
            _app.Use(handler);
            return this;
        }

        public IEngine MapRoute(string name, string template)
        {
            _builder.MapRoute(name, template);
            return this;
        }
    }
}