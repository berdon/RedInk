using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Engine.Plugin;
using System.Collections.Generic;
using Engine.Core.Models;
using Engine.Core;

namespace BlogPlugin
{
    public class BlogPlugin : IMvcPlugin
    {
        public BlogPlugin(IEngine engine) {
            
        }

        public void Initialize(IRouteBuilder builder)
        {
            builder.MapRoute(
                name: "post",
                template: "blog/{*postName}",
                defaults: new {
                    controller = "Post",
                    action = "Index"
                }
            );
        }
    }
}