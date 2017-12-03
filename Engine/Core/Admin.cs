using System.Collections.Generic;
using Engine.Core.Models;

namespace Engine.Core
{
    public class Admin : IAdmin
    {
        public IList<MenuCategory> Menu => throw new System.NotImplementedException();

        internal Admin(IInternalEngine internalEngine) {
            internalEngine.MapMvcRoute(
                name: "admin",
                template: "admin/{controller=Admin}/{action=Index}"
            );
        }

        public void RegisterMenuCategory(MenuCategory menuCategory)
        {
            throw new System.NotImplementedException();
        }
    }
}