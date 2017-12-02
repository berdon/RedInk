using System.Collections.Generic;

namespace Engine.Configuration
{
    public class EngineConfiguration
    {
        public WebsiteConfiguration Website { get; set; }
        public DatabaseConfiguration Database { get; set; }
    }
}