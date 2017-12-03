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
        public void Initialize(IEngine engine)
        {
            engine.Admin.RegisterMenuCategory(new MenuCategory {
                DisplayName = "Posts",
                Items = new List<MenuCategoryItem>(new [] {
                    new MenuCategoryItem {
                        DisplayName = "View Posts",
                        ViewComponent = typeof(AdminViewPostsViewComponent)
                    }
                })
            });
            engine.MapRoute("editPost", "blog/{postName}/edit", "PostIndex", true);
            engine.MapRoute("post", "blog/{postName}", "PostIndex");
        }
    }
}