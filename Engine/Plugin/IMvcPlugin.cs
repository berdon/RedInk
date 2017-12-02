using System.Threading.Tasks;
using Engine.Core;
using Microsoft.AspNetCore.Routing;

namespace Engine.Plugin
{
    public interface IMvcPlugin : IPlugin
    {
        void Initialize(IRouteBuilder builder);
    }
}