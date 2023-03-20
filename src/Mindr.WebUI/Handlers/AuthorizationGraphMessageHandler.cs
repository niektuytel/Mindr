using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Mindr.WebUI.Options;

namespace Mindr.WebUI.Handlers;

public class AuthorizationGraphMessageHandler : AuthorizationMessageHandler
{
    private readonly ILogger<AuthorizationGraphMessageHandler> logger = default!;

    public AuthorizationGraphMessageHandler(
        NavigationManager navigation,
        IAccessTokenProvider provider,
        IOptions<MicrosoftGraphOptions> options,
        ILoggerFactory loggerFactory
    ) : base(provider, navigation)
    {
        logger = loggerFactory.CreateLogger<AuthorizationGraphMessageHandler>() ?? throw new ArgumentNullException(nameof(logger));
        logger.LogDebug($"Setting up {nameof(AuthorizationGraphMessageHandler)} to authorize the base url: {options.Value.BaseUrl}");

        ConfigureHandler(authorizedUrls: new[] { options.Value.BaseUrl }, scopes: options.Value.Scopes);
    }
}
