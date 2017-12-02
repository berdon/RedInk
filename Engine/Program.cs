using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Engine.Configuration;
using Microsoft.Extensions.Configuration.Yaml;

namespace Engine
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddYamlFile("appsettings.yml")
                .Build();

            var engineConfiguration = Startup.Configuration = new EngineConfiguration();
            ConfigurationBinder.Bind(configuration, engineConfiguration);

            return WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureServices(services => services.AddSingleton(engineConfiguration))
                .UseUrls(engineConfiguration.Website.Urls())
                .Build();
        }
    }
}
