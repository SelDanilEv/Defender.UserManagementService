using Defender.UserManagement.Application.Helpers;
using Serilog;

namespace Defender.UserManagement.WebUI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        SimpleLogger.Log("Starting Service");

        var host = CreateHostBuilder(args).Build();
        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseKestrel(server => server.AddServerHeader = false)
                    .UseStartup<Startup>();
            });
}