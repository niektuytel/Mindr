using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using System.Text.Json.Nodes;
using System.Text.Json;
using Mindr.WebAssembly.Client.Extensions;
using System.Net.Http;
using Microsoft.JSInterop;
using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.Models.DTO.Personal;
using MudBlazor;
using Mindr.Domain.Models.DTO.Connector;
using static MudBlazor.CategoryTypes;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components;

public partial class GoogleAuthentication
{
    private static readonly string BaseUri = "https://accounts.google.com/o/oauth2";

    private string? _code { get; set; }

    private string? _scope { get; set; }

    [Parameter, EditorRequired]
    public RenderFragment? AuthenticatedView { get; set; } = default!;

    [Parameter, EditorRequired]
    public string RedirectUri { get; set; } = default!;

    [Parameter, EditorRequired]
    public string ClientSecret { get; set; } = default!;

    [Parameter, EditorRequired]
    public string ClientId { get; set; } = default!;

    [Parameter, EditorRequired]
    public string Scopes { get; set; } = default!;

    [Parameter]
    public string AccessType { get; set; } = "online";

    [Parameter]
    public string ResponseType { get; set; } = "code";

    [Inject]
    public IJSRuntime JSRuntime { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    [Inject]
    public IHttpClientFactory HttpClientFactory { get; set; } = default!;

    [Inject]
    public IApiPersonalCredentialClient CredentialClient { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    public string AccessToken { get; set; } = default!;

    public int ExpiresIn { get; set; } = default!;
    
    public string RefreshToken { get; set; } = default!;
    
    public string Scope { get; set; } = default!;
    
    public string TokenType { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _code = NavigationManager.ExtractQueryStringByKey<string>("code");
        _scope = NavigationManager.ExtractQueryStringByKey<string>("scope");

        if (!string.IsNullOrEmpty(_code) && !string.IsNullOrEmpty(_scope))
        {
            // Request accessToken
            var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUri}/token")
            {
                Content = new MultipartFormDataContent
            {
                { new StringContent("authorization_code"), "grant_type" },
                { new StringContent(_code), "code" },
                { new StringContent(ClientId), "client_id" },
                { new StringContent(ClientSecret), "client_secret" },
                { new StringContent(RedirectUri), "redirect_uri" }
            }
            };

            var httpClient = HttpClientFactory.CreateClient();
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();
            //{
            //    "access_token": "ya29.a0AWY7CklqaLHr3A6x_du7-JrtifzPBVTMAapV6zjTEJgPZWiGcdbAPoGYs9m8h4dXC5tM5eAZPMys2ooPrs-EYUd25wXKPS8uLg3TdSpmKKWLPH0YghddBj60ZxbhUGYGfxMytqFMMJt0f71oa12g4I7m1WAraCgYKATQSARESFQG1tDrpZu-pcnhWAKCbDk_1Gv31CA0163",
            //    "expires_in": 3599,
            //    "refresh_token": "1//09f8d-mKU1D2RCgYIARAAGAkSNwF-L9IrZ_GsMEG0Z-UsAuFJvLVh7y1bW0jr83HIOOfybWJ6OmL1M74lOpjH1_BQzjAtdzPWCu4",
            //    "scope": "https://www.googleapis.com/auth/calendar",
            //    "token_type": "Bearer"
            //}
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(jsonString);
            AccessToken = jsonObject!["access_token"]!.GetValue<string>();
            ExpiresIn = jsonObject!["expires_in"]!.GetValue<int>();
            RefreshToken = jsonObject!["refresh_token"]!.GetValue<string>();
            Scope = jsonObject!["scope"]!.GetValue<string>();
            TokenType = jsonObject!["token_type"]!.GetValue<string>();

            await CreatePersonalCredential();
        }
    }

    private async Task CreatePersonalCredential()
    {
        var response = await CredentialClient.Create(new PersonalCredentialDTO()
        {
            Target = Domain.Enums.CredentialTarget.GoogleCalendar,
            AccessToken = AccessToken,
            ExpiresIn = ExpiresIn,
            RefreshToken = RefreshToken,
            Scope = Scope,
            TokenType = TokenType
        });
        (var data, var error) = response.AsTuple();

        if (!string.IsNullOrEmpty(error))
        {
            Snackbar.Add(error, Severity.Error);
        }
        else if (data != null)
        {
            Snackbar.Add("Successfully bind google account", Severity.Success);
        }

        base.StateHasChanged();
    }

    public async Task<bool> HandleConsent()
    {
        if (string.IsNullOrEmpty(_code) || string.IsNullOrEmpty(_scope))
        {
            // Request consent
            var consentUri = $"{BaseUri}/v2/auth?scope={Scopes}&response_type={ResponseType}&access_type={AccessType}&redirect_uri={RedirectUri}&client_id={ClientId}&prompt=select_account";
            await JSRuntime.InvokeAsync<object>("open", consentUri);//, "_blank");// Ok but need to be as iframe than

            return true;
        }

        return false;
    }

}
