using DutchGrit.Afas;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mindr.Core.Models.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mindr.WebUI.Services;

public class ConnectorClient : IConnectorClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;
    private readonly IAccessTokenProvider _tokenProvider;


    public ConnectorClient(IHttpClientFactory factory, IConfiguration configuration, IAccessTokenProvider tokenProvider)
    {
        _httpClient = factory.CreateClient("ProductsApi");
        _baseUrl = configuration["BaseUrl"];
        _tokenProvider = tokenProvider;
    }

    public async Task<HttpResponseMessage> GetAll(string query = "", string eventId = "")
    {
        if (!string.IsNullOrEmpty(query))
        {
            query = $"&query={query}";
        }

        if (!string.IsNullOrEmpty(eventId))
        {
            eventId = $"&eventId={eventId}";
        }

        var tokenResult = await _tokenProvider.RequestAccessToken(new AccessTokenRequestOptions
        {
            Scopes = new[] { "api://832f0468-7f76-4fb3-8d5c-7e5bd70d17ea/access_as_user" }
        });

        if (tokenResult.TryGetToken(out var accessToken))
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/connector?{query}{eventId}");
            request.Headers.Add("Authorization", $"Bearer {accessToken.Value}");

            var res = await _httpClient.SendAsync(request);
            return res;
        }

        return null;
    }

}
