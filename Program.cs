using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace TurboCoConsole
{
    public class Program
    {
        public static void Main(
            string[] args
        )
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(
            string[] args
        ) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder =>
                    {
                        webBuilder
                            .ConfigureAppConfiguration(
                                c => c
                                    .AddJsonFile("appsettings.secrets.json", true)
                            )
                            .UseStartup<Startup>();
                    }
                );
    }
}