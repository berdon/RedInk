using System;
using System.Collections.Generic;

namespace Engine.Layout
{
    public interface ILayout
    {
         string View { get; }
         HashSet<Type> Plugins { get; }
    }
}