using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Fast.Components.FluentUI;
using Mindr.HttpRunner.Services;
using Mindr.WebUI;
using Mindr.WebUI.Extensions;
using Mindr.WebUI.Handlers;
using Mindr.WebUI.Models.Options;
using Mindr.WebUI.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// App settings
builder.Services.Configure<MicrosoftGraphOptions>(builder.Configuration);
builder.Services.Configure<ApiOptions>(builder.Configuration);

// HTTP clients
builder.Services.AddTransient<AuthorizationGraphMessageHandler>();
builder.Services.AddHttpClient(nameof(AuthorizationGraphMessageHandler), client => client.BaseAddress = new Uri(builder.Configuration["MicrosoftGraph:BaseUrl"]!))
                .AddHttpMessageHandler<AuthorizationGraphMessageHandler>();

builder.Services.AddScoped<AuthorizationApiMessageHandler>();
builder.Services.AddHttpClient(nameof(AuthorizationApiMessageHandler), client => client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!))
                .AddHttpMessageHandler<AuthorizationApiMessageHandler>();

// Services
builder.Services.AddTransient<IHttpAgendaClient, HttpAgendaClient>();
builder.Services.AddTransient<IHttpConnectorClient, HttpConnectorClient>();
builder.Services.AddTransient<IHttpConnectorEventClient, HttpConnectorEventClient>();
builder.Services.AddTransient<IHttpRunnerFactory, HttpRunnerFactory>();
builder.Services.AddTransient<IHttpRunnerClient, HttpRunnerClient>();

// Authentication
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

    // https://github.com/dotnet/aspnetcore/issues/39104#issuecomment-1288271430
    options.ProviderOptions.LoginMode = "redirect";
    options.ProviderOptions.Cache.CacheLocation = "localStorage";
});

// Design
builder.Services.AddFluentUIComponents();
builder.Services.AddBlazorDragDrop();

await builder.Build().RunAsync();
