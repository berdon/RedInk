using Engine.Core.Models;

namespace Engine.Core
{
    public class Admin : IAdmin
    {
        public Admin(IEngine engine) {
            engine.MapRoute(
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