using Mindr.WebUI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core;
using Mindr.WebUI.Services;
using Mindr.Core.Services.Connector;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTransient<GraphAuthorizationMessageHandler>();
builder.Services.AddHttpClient("GraphAPI", client => client.BaseAddress = new Uri(builder.Configuration.GetSection("MicrosoftGraph")["BaseUrl"]))
                .AddHttpMessageHandler<GraphAuthorizationMessageHandler>();

builder.Services.AddScoped<ApiOneAuthorizationRequestMessageHandler>();
builder.Services.AddHttpClient("ProductsApi", httpClient => httpClient.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<ApiOneAuthorizationRequestMessageHandler>();





builder.Services.AddFluentUIComponents();
builder.Services.AddBlazorDragDrop();
//If you're following Microsoft Doc for custom User Account Claims through the GraphApi, your Add Msal should look like this:

//Msal registration with custom User claims:

//builder.Services.AddMsalAuthentication<RemoteAuthenticationState, RemoteUserAccount>(options =>
//{
//    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
//})
//.AddAccountClaimsPrincipalFactory<RemoteAuthenticationState, RemoteUserAccount, GraphUserAccountFactory>();
builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);

    // https://github.com/dotnet/aspnetcore/issues/39104#issuecomment-1288271430
    options.ProviderOptions.LoginMode = "redirect";
    options.ProviderOptions.Cache.CacheLocation = "localStorage";

});

builder.Services.AddScoped<GraphClientFactory>();

builder.Services.AddTransient<CalendarController>();
builder.Services.AddTransient<IMicrosoftCalendarEventsProvider, MicrosoftCalendarEventsProvider>();
builder.Services.AddTransient<IHttpCollectionFactory, HttpCollectionFactory>();
builder.Services.AddTransient<IHttpCollectionClient, HttpCollectionClient>();

// WebUI
builder.Services.AddTransient<IConnectorHookClient, ConnectorHookClient>();
builder.Services.AddTransient<IConnectorClient, ConnectorClient>();


//builder.Services.AddSingleton<IAccessTokenProviderAccessor, AccessTokenProviderAccessor>();

await builder.Build().RunAsync();
