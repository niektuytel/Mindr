using Mindr.WebUI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core;
using Mindr.Core.Services;
using Mindr.Core.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Default", c =>
{
    c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});

builder.Services.AddFluentUIComponents();
builder.Services.AddBlazorDragDrop();
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/Calendars.ReadWrite");
    //options.ProviderOptions.DefaultAccessTokenScopes.Add("api://832f0468-7f76-4fb3-8d5c-7e5bd70d17ea/access_as_user Calendars.ReadWrite");

    // https://github.com/dotnet/aspnetcore/issues/39104#issuecomment-1288271430
    options.ProviderOptions.LoginMode = "redirect";
    options.ProviderOptions.Cache.CacheLocation = "localStorage";
});
builder.Services.AddSingleton<IConnectorHookClient, ConnectorHookClient>();


builder.Services.AddTransient<IMicrosoftGraphProvider, MicrosoftGraphProvider>();
builder.Services.AddTransient<CalendarController>();
builder.Services.AddSingleton<IHttpCollectionFactory, HttpCollectionFactory>();
builder.Services.AddSingleton<IHttpCollectionClient, HttpCollectionClient>();



await builder.Build().RunAsync();
