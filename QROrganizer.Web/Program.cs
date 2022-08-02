using System;
using QROrganizer.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace QROrganizer.Web;

public static class Program
{
    public static void Main(string[] args)
    {
        var host = BuildWebHost(args);

        // https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/#move-database-initialization-code
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            // Run database migrations.
            AppDbContext db = services.GetService<AppDbContext>();
            db.Initialize();
        }
        host.Run();
    }

    public static IWebHost BuildWebHost(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseWebRoot("wwwroot") // Prevents ASP.NET Core from ignoring wwwroot if it doesn't exist at startup.
            .ConfigureAppConfiguration((builder, config) =>
            {
                config
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
#if DEBUG
                if (Environment.OSVersion.Platform == PlatformID.Unix)
                {
                    config.AddJsonFile("appsettings.dockerdb.json");
                }
#endif
                config.AddEnvironmentVariables();
            })
            .ConfigureLogging(logging =>
            {
                logging.ClearProviders();

#if !DEBUG
                logging.AddNLog("nlog.config");
#endif

                logging.AddConsole();
                logging.SetMinimumLevel(LogLevel.Trace);
            })
            .UseStartup<Startup>()
            .UseNLog()
            .Build();
}
