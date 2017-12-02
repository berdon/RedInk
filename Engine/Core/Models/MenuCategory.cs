using System.Collections.Generic;

namespace Engine.Core.Models
{
    public class MenuCategory
    {
        public string DisplayName { get; set; }
        public IList<MenuCategoryItem> Items { get; set; }
    }
}