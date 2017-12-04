using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Engine.Layout
{
    public class MockLayoutManager : ILayoutManager
    {
        private readonly IServiceProvider _provider;
        private IList<ILayout> _layouts;
        public IList<ILayout> Layouts
        {
            get
            {
                if (_layouts is default)
                {
                    _layouts = new List<ILayout>(new[] {
                        ActivatorUtilities.CreateInstance<BlogPlugin.Layouts.Default.DefaultLayout>(_provider)
                    });
                }

                return _layouts;
            }
        }

        public MockLayoutManager(IServiceProvider provider)
        {
            this._provider = provider;

        }

        public ILayout LayoutForRoute(string name)
        {
            return Layouts.First();
        }

        public string PathForLayoutView(ILayout layout)
        {
            return "/Plugin/BlogPlugin/Layouts/Default/" + layout.View;
        }

        public IEnumerable<string> ComponentsForName(string name)
        {
            if (name.Equals("header")) {
                return new [] { "Header", "Header" };
            }

            if (name.Equals("footer")) {
                return new [] { "Footer", "Footer" };
            }

            throw new Exception();
        }
    }
}