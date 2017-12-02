using System.Collections.Generic;

namespace Engine.Configuration
{
    public class DatabaseConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 5432;
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        private string _connectionString;
        public string ConnectionString() {
            if (_connectionString is default) {
                _connectionString = $"Server={Host};Port={Port};Database={Database};User Id={Username};Password={Password};";
            }

            return _connectionString;
        }
    }
}