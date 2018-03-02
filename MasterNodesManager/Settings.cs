using System.IO;
using Microsoft.Extensions.Configuration;

namespace MasterNodesManager
{
    static class Settings
    {
        public static IConfiguration Load()
        {
            
            //// config
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            //Console.WriteLine($"option1 = {configuration["Option1"]}");
            //Console.WriteLine($"option2 = {configuration["option2"]}");
            //Console.WriteLine(
            //    $"suboption1 = {configuration["subsection:suboption1"]}");
            //Console.WriteLine();

            //Console.WriteLine("Wizards:");
            //Console.Write($"{configuration["wizards:0:Name"]}, ");
            //Console.WriteLine($"age {configuration["wizards:0:Age"]}");
            //Console.Write($"{configuration["wizards:1:Name"]}, ");
            //Console.WriteLine($"age {configuration["wizards:1:Age"]}");
            //Console.WriteLine();

            //Console.WriteLine("Press a key...");
            //Console.ReadKey();

            return configuration;

        }
    }
}
