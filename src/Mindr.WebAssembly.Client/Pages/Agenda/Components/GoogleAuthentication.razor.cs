using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using System.Text.Json.Nodes;
using System.Text.Json;
using Mindr.WebAssembly.Client.Extensions;
using System.Net.Http;
using Microsoft.JSInterop;

namespace Mindr.WebAssembly.Client.Pages.Agenda.Components;

public partial class GoogleAuthentication : FluentComponentBase
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

    public string AccessToken { get; set; } = default!;

    protected override async Task OnInitializedAsync()
    {
        _code = NavigationManager.ExtractQueryStringByKey<string>("code");
        _scope = NavigationManager.ExtractQueryStringByKey<string>("scope");

        if (!string.IsNullOrEmpty(_code) || !string.IsNullOrEmpty(_scope))
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

            var httpClient = HttpClientFactory.CreateClient(nameof(GoogleCalendarDialog));
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            //{
            //    "access_token": "ya29.a0Ael9sCMA4dpOa3KsaesbdeSDt7PFJp4UcGlyaF0XjktteENXAw6SIoeq5hgn3CT7pLogIz0G9Nyhne55LYnYNyzklQqhtvvYzsaCgYKAfwSARESFQF4udJhsnAsZsf1cB3fRK9fUwmeTw0163",
            //    "expires_in": 3463,
            //    "scope": "",
            //    "token_type": "Bearer"
            //}
            var jsonString = await response.Content.ReadAsStringAsync();
            var jsonObject = JsonSerializer.Deserialize<JsonObject>(jsonString);
            AccessToken = jsonObject!["access_token"]!.GetValue<string>();
        }
    }

    public async Task<bool> HandleConsent()
    {
        if (string.IsNullOrEmpty(_code) || string.IsNullOrEmpty(_scope))
        {
            // Request consent
            var consentUri = $"{BaseUri}/v2/auth?scope={Scopes}&response_type={ResponseType}&access_type={AccessType}&redirect_uri={RedirectUri}&client_id={ClientId}";
            await JSRuntime.InvokeAsync<object>("open", consentUri);//, "_blank");// Ok but need to be as iframe than

            return false;
        }

        return true;
    }

}
