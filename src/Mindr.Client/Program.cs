using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Mindr.Client;
using Mindr.Client.Models.Options;
using Mindr.Client.Handlers;
using Mindr.Client.Services;
using Mindr.HttpRunner.Services;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Client.Extensions;
using Microsoft.AspNetCore.Components.Authorization;

namespace Mindr.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        // Options
        builder.Services.Configure<MicrosoftGraphOptions>(builder.Configuration);
        builder.Services.Configure<ApiOptions>(builder.Configuration);

        // Services
        builder.Services.AddTransient<IHttpAgendaClient, HttpAgendaClient>();
        builder.Services.AddTransient<IHttpConnectorClient, HttpConnectorClient>();
        builder.Services.AddTransient<IHttpConnectorEventClient, HttpConnectorEventClient>();
        builder.Services.AddTransient<IHttpRunnerFactory, HttpRunnerFactory>();
        builder.Services.AddTransient<IHttpRunnerClient, HttpRunnerClient>();

        // TODO: Deprecated
        builder.Services.AddTransient<AuthorizationGraphMessageHandler>();
        builder.Services.AddHttpClient(nameof(AuthorizationGraphMessageHandler), client => client.BaseAddress = new Uri(builder.Configuration["MicrosoftGraph:BaseUrl"]!))
                        .AddHttpMessageHandler<AuthorizationGraphMessageHandler>();

        builder.Services.AddScoped<AuthorizationApiMessageHandler>();
        builder.Services.AddHttpClient(nameof(AuthorizationApiMessageHandler), client => client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!))
                        .AddHttpMessageHandler<AuthorizationApiMessageHandler>();

        // Authentication
        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Mindr.ServerAPI"));
        builder.Services.AddHttpClient("Mindr.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        builder.Services.AddHttpClient();
        builder.Services.AddApiAuthorization(); 
        builder.Services.AddFluentUIComponents();
        builder.Services.AddBlazorDragDrop();
        await builder.Build().RunAsync();
    }
}