using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Engine.Layout
{
    public interface ILayoutManager
    {
         IList<ILayout> Layouts { get; }
        //  IList<ILayoutComponent> LayoutComponents { get; }
         ILayout LayoutForRoute(string name);
         string PathForLayoutView(ILayout layout);
         IEnumerable<string> ComponentsForName(string name);
    }
}