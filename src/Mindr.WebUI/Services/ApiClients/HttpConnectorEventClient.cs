using Azure.Core;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Mindr.Core.Models.Connector;
using Mindr.WebUI.Handlers;
using Mindr.WebUI.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mindr.WebUI.Services.ApiClients;

public class HttpConnectorEventClient : IHttpConnectorEventClient
{
    private readonly HttpClient _httpClient;
    private readonly ApiOptions _options;
    private readonly IAccessTokenProvider _tokenProvider;

    public HttpConnectorEventClient(IHttpClientFactory factory, IAccessTokenProvider tokenProvider, IOptions<ApiOptions> options)
    {
        _httpClient = factory.CreateClient(nameof(AuthorizationApiMessageHandler));
        _tokenProvider = tokenProvider;
        _options = options.Value!;
    }

    private string ControllerUrl => $"{_options.BaseUrl}/connectorevent";

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

    public async Task<HttpResponseMessage?> GetAll()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{ControllerUrl}");
        var validAuth = await TrySetAuthorization(request);
        if (!validAuth) return null;

        var response = await _httpClient.SendAsync(request);
        return response;
    }

    public async Task<HttpResponseMessage?> Upsert(ConnectorEvent @event)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, ControllerUrl);
        var validAuth = await TrySetAuthorization(request);
        if (!validAuth) return null;

        request.Headers.Add("accept", "*/*");

        var json = JsonConvert.SerializeObject(@event);
        request.Content = new StringContent(json);

        var response = await _httpClient.SendAsync(request);
        return response;
    }

    public async Task<HttpResponseMessage?> Delete(Guid eventid)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{ControllerUrl}/{eventid}");
        var validAuth = await TrySetAuthorization(request);
        if (!validAuth) return null;

        var response = await _httpClient.SendAsync(request);
        return response;
    }

}
