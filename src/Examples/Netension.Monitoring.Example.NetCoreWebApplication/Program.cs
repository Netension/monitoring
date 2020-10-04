using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Netension.Monitoring.Example.NetCoreWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole(options =>
                    {
                        options.DisableColors = false;
                    })
                    .SetMinimumLevel(LogLevel.Trace));
                    webBuilder.ConfigureServices(services => services.AddSingleton(loggerFactory));
                    Startup.LoggerFactory = loggerFactory;
                    webBuilder.UseStartup<Startup>();
                });
    }
}
