using System;
using System.Security.Cryptography;
using Engine.Core;
using Engine.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Engine.Plugin.ApiPlugin
{
    public class ApiPlugin : IRequestPlugin
    {
        // private Type[] _apiModels;

        public void Initialize(IApplicationBuilder app)
        {
            var apiRouter = new RouteHandler(context => {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync(
                    $"Hello! Route values: {string.Join(", ", routeValues)}");
            });

            var routeBuilder = new RouteBuilder(app, apiRouter);

            routeBuilder.MapRoute(
                "Track Package Route",
                "package/{operation:regex(^(track|create|detonate)$)}/{id:int}");

            routeBuilder.MapGet("hash/{password}", context =>
            {
                var password = context.GetRouteValue("password") as string;

                string salt = null;
                var hash = password.Hash(ref salt);

                return context.Response.WriteAsync($"{hash}|{salt}");
            });

            var routes = routeBuilder.Build();
            app.UseRouter(routes);
        }
    }
}