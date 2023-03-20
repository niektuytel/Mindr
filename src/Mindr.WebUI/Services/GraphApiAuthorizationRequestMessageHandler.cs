using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;

namespace Mindr.WebUI.Services
{
    // https://stackoverflow.com/questions/68553637/blazor-standalone-wasm-unable-to-get-access-token-with-msal
    public class GraphApiAuthorizationRequestMessageHandler : AuthorizationMessageHandler
    {
        public GraphApiAuthorizationRequestMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager
            )
            : base(provider, navigationManager)
        {
            ConfigureHandler(
               authorizedUrls: new[] { "https://graph.microsoft.com" },
               scopes: new[] { "User.Read", "MailboxSettings.Read", "Calendars.Read" });
        }
    }
}
