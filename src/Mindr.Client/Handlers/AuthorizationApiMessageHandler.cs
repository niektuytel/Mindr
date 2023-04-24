using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Mindr.Client.Models.Options;

namespace Mindr.Client.Handlers
{
    // https://stackoverflow.com/questions/68553637/blazor-standalone-wasm-unable-to-get-access-token-with-msal
    public class AuthorizationApiMessageHandler : AuthorizationMessageHandler
    {
        private readonly ILogger<AuthorizationApiMessageHandler> logger = default!;

        public AuthorizationApiMessageHandler(
            NavigationManager navigation,
            IAccessTokenProvider provider,
            IOptions<ApiOptions> options,
            IWebAssemblyHostEnvironment hostEnv,
            ILoggerFactory loggerFactory
        ) : base(provider, navigation)
        {
            logger = loggerFactory.CreateLogger<AuthorizationApiMessageHandler>() ?? throw new ArgumentNullException(nameof(logger));
            logger.LogDebug($"Setting up {nameof(AuthorizationApiMessageHandler)} to authorize the base url: {hostEnv.BaseAddress}");

            ConfigureHandler(authorizedUrls: new[] { hostEnv.BaseAddress }, scopes: options.Value.Scopes);
        }
    }
}
