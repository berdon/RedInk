using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Engine.Plugin
{
    public class MockPluginManager : IPluginManager
    {
        private readonly IServiceProvider _provider;
        private IList<IPlugin> _plugins;
        public IList<IPlugin> Plugins
        {
            get
            {
                if (_plugins is default) {
                    _plugins = new List<IPlugin>();
                    _plugins.Add(ActivatorUtilities.CreateInstance<ApiPlugin.ApiPlugin>(_provider));
                    _plugins.Add(ActivatorUtilities.CreateInstance<BlogPlugin.BlogPlugin>(_provider));
                }

                return _plugins;
            }
        }

        public MockPluginManager(IServiceProvider provider)
        {
            _provider = provider;
        }
    }
}