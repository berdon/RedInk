using System.Collections.Generic;
using Engine.Core.Models;

namespace Engine.Core
{
    public class Admin : IAdmin
    {
        public IList<MenuCategory> Menu { get; set; } = new List<MenuCategory>();

        internal Admin(IInternalEngine internalEngine) {
            internalEngine.MapMvcRoute(
                name: "admin",
                template: "admin/{controller=Admin}/{action=Index}"
            );
        }

        public void RegisterMenuCategory(MenuCategory menuCategory)
        {
            Menu.Add(menuCategory);
        }
    }
}