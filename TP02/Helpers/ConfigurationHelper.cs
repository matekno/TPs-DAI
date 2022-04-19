using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Pizzas.API.Helpers
{
    public static class ConfigurationHelper
    {

        // para obtener la carpeta de logs del appsettings.json:
        // string LogFolder = ConfigurationHelper.GetConfiguration().GetValue<string>("CustomLog:LogFolder");
        public static IConfiguration GetConfiguration()
        {
            IConfiguration config;
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            config = builder.Build();
            return config;
        }

        public static string GetLogFolder(){
            return ConfigurationHelper.GetConfiguration().GetValue<string>("CustomLog:LogFolder");
        }
    }
}
