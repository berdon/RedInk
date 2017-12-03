using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Engine.Core
{
    public interface IEngine
    {
        IAdmin Admin { get; }
        void Initialize(IApplicationBuilder builder, IRouteBuilder routeBuilder);
        IEngine MapRoute(string name, string template, string viewComponent, bool requireAuthenticated = false);
        IEngine MapRoute(string route, Func<RequestDelegate, RequestDelegate> handler);
    }

    internal interface IInternalEngine {
        IEngine MapMvcRoute(string name, string template);
    }
}