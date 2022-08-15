namespace LibraryApp.Api;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // using (var scope = host.Services.CreateScope())
        // {
        //     await scope.ServiceProvider.GetRequiredService<DataBaseInitializer>().InitializeDbAsync();
        // }

        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) 
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}