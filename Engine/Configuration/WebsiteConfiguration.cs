using System.Collections.Generic;

namespace Engine.Configuration
{
    public class WebsiteConfiguration
    {
        public bool UseHttps { get; set; }
        public bool RequireHttps { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }

        private string[] _urls;

        public string[] Urls() {
            if (_urls == null) {
                var urls = new List<string>();
                if (UseHttps) urls.Add($"https://{Host}:{Port}");
                if (!UseHttps && !RequireHttps) urls.Add($"http://{Host}:{Port}");
                _urls = urls.ToArray();
            }

            return _urls;
        }
    }
}