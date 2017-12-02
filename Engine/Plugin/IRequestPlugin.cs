using System.Threading.Tasks;
using Engine.Core;
using Microsoft.AspNetCore.Builder;

namespace Engine.Plugin
{
    public interface IRequestPlugin : IPlugin
    {
         void Initialize(IApplicationBuilder engine);
    }
}