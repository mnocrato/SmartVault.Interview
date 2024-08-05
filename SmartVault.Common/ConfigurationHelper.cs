using Microsoft.Extensions.Configuration;
using System.IO;

namespace SmartVault.Common
{
    public static class ConfigurationHelper
    {
        public static IConfigurationRoot GetConfiguration()
        {
            var projectBasePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "SmartVault.DataGeneration", "bin", "Debug", "net5.0"));

            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectBasePath)
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration;
        }

        public static string GetConnectionString()
        {
            var configuration = GetConfiguration();
            return configuration.GetConnectionString("DefaultConnection");
        }
    }
}