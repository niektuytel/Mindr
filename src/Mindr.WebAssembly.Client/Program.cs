using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Fast.Components.FluentUI;
using Mindr.WebAssembly.Client.Extensions;
using Mindr.WebAssembly.Client.Handlers;
using Mindr.WebAssembly.Client.Models.Options;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.HttpRunner.Services;
using Mindr.WebAssembly.Client.Providers;

namespace Mindr.WebAssembly.Client;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddOptions();
        builder.Services.AddHttpClient();
        builder.Services.AddFluentUIComponents();
        builder.Services.AddBlazorDragDrop();

        // Authentication
        builder.Services.AddAuthorizationCore();
        builder.Services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();
        builder.Services.TryAddSingleton(provider => (HostAuthenticationStateProvider) provider.GetRequiredService<AuthenticationStateProvider>());

        // HTTP connections
        builder.Services.AddHttpClient("default", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
        builder.Services.AddTransient(provider => provider.GetRequiredService<IHttpClientFactory>().CreateClient("default"));

        builder.Services.AddTransient<AuthorizedMindrApiHandler>();
        builder.Services.AddHttpClient(nameof(AuthorizedMindrApiHandler), client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                        .AddHttpMessageHandler<AuthorizedMindrApiHandler>();

        // Services
        builder.Services.AddScoped<IApiConnectorClient, ApiConnectorClient>();
        builder.Services.AddScoped<IApiConnectorEventClient, ApiConnectorEventClient>();
        builder.Services.AddScoped<IHttpRunnerFactory, HttpRunnerFactory>();
        builder.Services.AddScoped<IHttpRunnerClient, HttpRunnerClient>();

        await builder.Build().RunAsync();
    }
}
