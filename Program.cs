using System;
using System.IO;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace redis
{
    class Program
    {
        
        private static IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile(Path.GetFullPath(Directory.GetCurrentDirectory()) + "/appsettings.json");
        private static IConfiguration config = builder.Build();

        private static string connectionString = config.GetConnectionString("redis");
        static void Main(string[] args)
        {
            IDatabase cache = lazyConnection.Value.GetDatabase();

            Console.WriteLine("Reading Cache: " + cache.StringGet("Session333").ToString());
            Console.WriteLine("Writing Cache: " + cache.StringSet("Session333", "Writing something to Redis"));
            Console.WriteLine("Reading Cache: " + cache.StringGet("Session333").ToString());
            cache.KeyExpire("Session333", DateTime.Now.AddMinutes(1));

            lazyConnection.Value.Dispose();

            Console.WriteLine();
            Console.WriteLine();
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() => 
        {
            return ConnectionMultiplexer.Connect(connectionString);
        });

        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
