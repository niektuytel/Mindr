using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Mindr.WebUI.Configurations
{
    // https://stackoverflow.com/questions/68553637/blazor-standalone-wasm-unable-to-get-access-token-with-msal
    public class ApiOneAuthorizationRequestMessageHandler : AuthorizationMessageHandler
    {
        // ILogger if you want..
        private readonly ILogger<ApiOneAuthorizationRequestMessageHandler> logger = default!;
        public ApiOneAuthorizationRequestMessageHandler(IAccessTokenProvider provider,
            NavigationManager navigationManager,
            ILoggerFactory loggerFactory
        ) : base(provider, navigationManager)
        {
            logger = loggerFactory.CreateLogger<ApiOneAuthorizationRequestMessageHandler>() ?? throw new ArgumentNullException(nameof(logger));

            logger.LogDebug($"Setting up {nameof(ApiOneAuthorizationRequestMessageHandler)} to authorize the base url: {"https://localhost:7247/"}");
            ConfigureHandler(
               authorizedUrls: new[] { "https://localhost:7247" },
               scopes: new[] { "api://832f0468-7f76-4fb3-8d5c-7e5bd70d17ea/access_as_user" });
        }
    }
}
