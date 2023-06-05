using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Mindr.OpenIDConnect;

namespace Mindr.Server;

public static class Program
{
    public static void Main(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetDevelopmentAppsettings(hostingContext.HostingEnvironment.EnvironmentName);
            })
            .ConfigureWebHostDefaults(options => options.UseStartup<Startup>())
            .Build()
            .Run();
}
