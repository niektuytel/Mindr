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
using Mindr.WebAssembly.Client;

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
        //builder.Services.AddScoped<IHttpAgendaClient, HttpAgendaClient>();
        builder.Services.AddScoped<IHttpConnectorClient, HttpConnectorClient>();
        builder.Services.AddScoped<IHttpConnectorEventClient, HttpConnectorEventClient>();
        builder.Services.AddScoped<IHttpRunnerFactory, HttpRunnerFactory>();
        builder.Services.AddScoped<IHttpRunnerClient, HttpRunnerClient>();

        // TODO: Deprecated
        builder.Services.AddScoped<AuthorizationGraphMessageHandler>();
        builder.Services.AddHttpClient(nameof(AuthorizationGraphMessageHandler), client => client.BaseAddress = new Uri(builder.Configuration["MicrosoftGraph:BaseUrl"]!))
                        .AddHttpMessageHandler<AuthorizationGraphMessageHandler>();

        builder.Services.AddScoped<AuthorizationApiMessageHandler>();
        builder.Services.AddHttpClient(nameof(AuthorizationApiMessageHandler), client => client.BaseAddress = new Uri(builder.Configuration["Api:BaseUrl"]!))
                        .AddHttpMessageHandler<AuthorizationApiMessageHandler>();

        // Authentication
        builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Mindr.ServerAPI"));
        builder.Services.AddHttpClient("Mindr.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        builder.Services.AddOidcAuthentication(options =>
        {
            options.ProviderOptions.ClientId = "balosar-blazor-client";
            options.ProviderOptions.Authority = "https://localhost:44319/";
            options.ProviderOptions.ResponseType = "code";

            // Note: response_mode=fragment is the best option for a SPA. Unfortunately, the Blazor WASM
            // authentication stack is impacted by a bug that prevents it from correctly extracting
            // authorization error responses (e.g error=access_denied responses) from the URL fragment.
            // For more information about this bug, visit https://github.com/dotnet/aspnetcore/issues/28344.
            //
            options.ProviderOptions.ResponseMode = "query";
            options.AuthenticationPaths.RemoteRegisterPath = "https://localhost:44319/Identity/Account/Register";

            // Add the "roles" (OpenIddictConstants.Scopes.Roles) scope and the "role" (OpenIddictConstants.Claims.Role) claim
            // (the same ones used in the Startup class of the Server) in order for the roles to be validated.
            // See the Counter component for an example of how to use the Authorize attribute with roles
            options.ProviderOptions.DefaultScopes.Add("roles");
            options.UserOptions.RoleClaim = "role";
        });



        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy("AllowAll", builder =>
        //    {
        //        builder.AllowAnyOrigin()
        //               .AllowAnyMethod()
        //               .AllowAnyHeader();
        //    });
        //});


        builder.Services.AddHttpClient();
        builder.Services.AddApiAuthorization(); 
        builder.Services.AddFluentUIComponents();
        builder.Services.AddBlazorDragDrop();
        await builder.Build().RunAsync();
    }
}