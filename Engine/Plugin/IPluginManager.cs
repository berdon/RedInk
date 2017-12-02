using System.Collections.Generic;

namespace Engine.Plugin
{
    public interface IPluginManager
    {
        IList<IPlugin> Plugins { get; }
    }
}