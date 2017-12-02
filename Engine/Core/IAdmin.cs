using System.Collections.Generic;
using Engine.Core.Models;

namespace Engine.Core
{
    public interface IAdmin
    {
        IList<MenuCategory> Menu { get; }

        void RegisterMenuCategory(MenuCategory menuCategory);
    }
}