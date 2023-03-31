using DutchGrit.Afas;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Mindr.Core.Models.Connector;
using Mindr.WebUI.Handlers;
using Mindr.WebUI.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mindr.WebUI.Services.ApiClients;

public class HttpConnectorHttpClient : IHttpConnectorClient
{
    private readonly HttpClient _httpClient;
    private readonly ApiOptions _options;
    private readonly IAccessTokenProvider _tokenProvider;

    public HttpConnectorHttpClient(IHttpClientFactory factory, IAccessTokenProvider tokenProvider, IOptions<ApiOptions> options)
    {
        _httpClient = factory.CreateClient(nameof(AuthorizationApiMessageHandler));
        _tokenProvider = tokenProvider;
        _options = options.Value!;
    }

    private string ControllerUrl => $"{_options.BaseUrl}/connector";

    private async Task<bool> TrySetAuthorization(HttpRequestMessage request)
    {
        var options = new AccessTokenRequestOptions { Scopes = _options.Scopes };
        var tokenRequest = await _tokenProvider.RequestAccessToken(options);
        var isSuccessfull = tokenRequest.TryGetToken(out var accessToken);
        if (isSuccessfull)
        {
            request.Headers.Add("Authorization", $"Bearer {accessToken.Value}");
            return true;
        }

        return false;
    }

    public async Task<HttpResponseMessage?> GetAll(string query = "", string eventId = "")
    {
        if (!string.IsNullOrEmpty(query))
        {
            query = $"&query={query}";
        }

        if (!string.IsNullOrEmpty(eventId))
        {
            eventId = $"&eventId={eventId}";
        }

        var request = new HttpRequestMessage(HttpMethod.Get, $"{ControllerUrl}?{query}{eventId}");
        var validAuth = await TrySetAuthorization(request);
        if (!validAuth) return null;

        var response = await _httpClient.SendAsync(request);
        return response;
    }

    public async Task<HttpResponseMessage?> GetBriefly(string connectorId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{ControllerUrl}/{connectorId}");
        var validAuth = await TrySetAuthorization(request);
        if (!validAuth) return null;

        var response = await _httpClient.SendAsync(request);
        return response;
    }

    public async Task<HttpResponseMessage?> Create(Connector content)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, ControllerUrl);
        var validAuth = await TrySetAuthorization(request);
        if (!validAuth) return null;

        request.Headers.Add("accept", "*/*");

        var json = JsonConvert.SerializeObject(content);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.SendAsync(request);
        return response;
    }

    public async Task<HttpResponseMessage?> Delete(string connectorId)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{ControllerUrl}/{connectorId}");
        var validAuth = await TrySetAuthorization(request);
        if (!validAuth) return null;

        var response = await _httpClient.SendAsync(request);
        return response;
    }

}
