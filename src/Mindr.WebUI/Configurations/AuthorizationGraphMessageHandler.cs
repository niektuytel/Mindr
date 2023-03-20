using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components;

namespace Mindr.WebUI.Configurations;

public class AuthorizationGraphMessageHandler : AuthorizationMessageHandler
{
    public AuthorizationGraphMessageHandler(NavigationManager navigation, IAccessTokenProvider provider, IConfiguration config) 
        : base(provider, navigation)
    {
        var baseUrl = config.GetSection("MicrosoftGraph")["BaseUrl"];
        var scopes = config.GetSection("MicrosoftGraph:Scopes").Get<List<string>>();

        ConfigureHandler(authorizedUrls: new[] { baseUrl }, scopes: scopes);
    }
}
