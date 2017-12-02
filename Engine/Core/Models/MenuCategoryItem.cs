using System;

namespace Engine.Core.Models
{
    public class MenuCategoryItem
    {
        public string DisplayName { get; set; }
        public Type ViewComponent { get; set; }
    }
}