using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dantooine.WebAssembly.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Client.Extensions;
using Mindr.Client.Handlers;
using Mindr.Client.Models.Options;
using Mindr.Client.Services;
using Mindr.HttpRunner.Services;

namespace Dantooine.WebAssembly.Client;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.Services.AddOptions();
        builder.Services.AddAuthorizationCore();
        builder.Services.TryAddSingleton<AuthenticationStateProvider, HostAuthenticationStateProvider>();
        builder.Services.TryAddSingleton(provider => (HostAuthenticationStateProvider) provider.GetRequiredService<AuthenticationStateProvider>());
        builder.Services.AddTransient<AuthorizedHandler>();

        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // Options
        builder.Services.Configure<ApiOptions>(builder.Configuration);

        // Services
        builder.Services.AddScoped<IApiConnectorClient, ApiConnectorClient>();
        builder.Services.AddScoped<IApiConnectorEventClient, ApiConnectorEventClient>();
        builder.Services.AddScoped<IHttpRunnerFactory, HttpRunnerFactory>();
        builder.Services.AddScoped<IHttpRunnerClient, HttpRunnerClient>();

        //builder.Services.AddScoped<AuthorizationApiMessageHandler>();
        //builder.Services.AddHttpClient(nameof(AuthorizationApiMessageHandler), client => client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!))
        //                .AddHttpMessageHandler<AuthorizationApiMessageHandler>();

        builder.Services.AddHttpClient("default", client =>
        {
            client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        builder.Services.AddHttpClient("authorizedClient", client =>
        {
            client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }).AddHttpMessageHandler<AuthorizedHandler>();

        builder.Services.AddTransient(provider => provider.GetRequiredService<IHttpClientFactory>().CreateClient("default"));

        builder.Services.AddHttpClient();
        //builder.Services.AddApiAuthorization();
        builder.Services.AddFluentUIComponents();
        builder.Services.AddBlazorDragDrop();
        await builder.Build().RunAsync();
    }
}
