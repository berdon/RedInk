using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Engine.Core
{
    public interface IEngine
    {
        void Initialize(IApplicationBuilder builder, IRouteBuilder routeBuilder);
        IEngine MapRoute(string name, string template);
        IEngine MapRoute(string route, Func<RequestDelegate, RequestDelegate> handler);
    }
}