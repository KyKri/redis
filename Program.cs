using System;
using System.IO;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace redis
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile(Path.GetFullPath(Directory.GetCurrentDirectory()) + "/appsettings.json");
            IConfiguration config = builder.Build();

            string connectionString = config.GetConnectionString("redis");
        }
    }
}
