using System.Collections.Generic;
using System.Linq;

namespace Engine.Plugin
{
    public static class PluginExtensions
    {
        public static IEnumerable<TPlugin> Plugins<TPlugin>(this IPluginManager manager)
            where TPlugin : IPlugin
        {
            return manager.Plugins.Where(p => p is TPlugin).Select(p => (TPlugin) p);
        }

        public static TPlugin Plugin<TPlugin>(this IPluginManager manager) {
            return (TPlugin) manager.Plugins.FirstOrDefault(p => p is TPlugin);
        }
    }
}