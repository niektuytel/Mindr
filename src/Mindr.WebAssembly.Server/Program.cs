using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Mindr.WebAssembly.Server;

public static class Program
{
    public static void Main(string[] args) =>
        Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.SetDevelopmentAppsettings(hostingContext.HostingEnvironment.EnvironmentName);
        })
        .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>())
        .Build()
        .Run();

}
