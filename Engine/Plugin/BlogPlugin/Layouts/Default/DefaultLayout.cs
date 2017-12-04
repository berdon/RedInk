using System;
using System.Collections.Generic;
using Engine.Layout;

namespace BlogPlugin.Layouts.Default
{
    public class DefaultLayout : ILayout
    {
        public string View => "Views/Index.cshtml";

        public HashSet<Type> Plugins => new HashSet<Type>(new [] {
            typeof(BlogPlugin)
        });
    }
}