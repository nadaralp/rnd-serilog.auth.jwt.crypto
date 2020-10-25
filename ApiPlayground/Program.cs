using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Serilog;

namespace ApiPlayground
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // set up serilog here
            // we have to build the configuration here because we don't have a DI yet. so we can only ask for it later.
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            // Serilog.Aspnetcore
            // Serilog.Settings.Congiruration
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.
                 Configuration(configuration)
                //.WriteTo.File() -> That's configuration in code. We are going to configure in appsettigs.json
                .CreateLogger();

            // Information: In order to have serilog enrichers, we must add them.
            // Serilog.Enrichers.Environment
            // Serilog.Enrichers.Thread
            // Serilog.Enrichers.Process

            // Information: Serilog writes to "sinks". If you want to have unordinary ones you need to download nuget for them.

            CreateHostBuilder(args).Build().Run();

            // This is using the Serilog logger. When we call ILogger<> we would have the serilog logger.
            Log.Information("Applications starting up");
            //Log.CloseAndFlush();



        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {


            var host = Host.CreateDefaultBuilder(args)
                    // Will use serilog rather than .NET core built in system
                    .UseSerilog()
                    .ConfigureWebHostDefaults(webBuilder =>
                        {
                            webBuilder.UseStartup<Startup>();
                        });


            return host;

        }
    }
}
