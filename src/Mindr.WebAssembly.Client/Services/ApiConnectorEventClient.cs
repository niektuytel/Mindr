using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Mindr.WebAssembly.Client.Models.Options;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Handlers;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System;

using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Text.Json;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Services;

public class ApiConnectorEventClient : ApiClientBase, IApiConnectorEventClient
{
    private static readonly string Path = "api/connectorevent/personal";
    private static readonly string HttpClientName = nameof(AuthorizedMindrApiHandler);

    public ApiConnectorEventClient(IJSRuntime JSRuntime, IHttpClientFactory factory) 
        : base(JSRuntime, factory.CreateClient(HttpClientName))
    {
    }

    public async Task<JsonResponse<ConnectorEvent>> Get(string eventId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Path}/{eventId}");

        var response = await ApiRequest<ConnectorEvent>(request);
        return response;
    }

    public async Task<JsonResponse<IEnumerable<ConnectorEvent>>> GetAll(string query = "", string eventId = "")
    {
        var uri = $"{Path}";
        if (!string.IsNullOrEmpty(eventId))
        {
            uri += $"?eventId={eventId}";
        }
        else if (!string.IsNullOrEmpty(query))
        {
            uri += $"?query={query}";
        }

        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var response = await ApiRequest<IEnumerable<ConnectorEvent>>(request);
        return response;
    }

    public async Task<JsonResponse<ConnectorEvent>> Insert(ConnectorEvent connectorEvent)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, Path);
        request.Headers.Add("accept", "*/*");

        var content = JsonSerializer.Serialize(connectorEvent);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await ApiRequest<ConnectorEvent>(request);
        return response;
    }

    public async Task<JsonResponse<ConnectorEvent>> Update(Guid eventid, ConnectorEvent connectorEvent)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{Path}/{eventid}");
        request.Headers.Add("accept", "*/*");

        var content = JsonSerializer.Serialize(connectorEvent);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await ApiRequest<ConnectorEvent>(request);
        return response;
    }

    public async Task<JsonResponse<ConnectorEvent>> Delete(Guid eventid)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{Path}/{eventid}");
        
        var response = await ApiRequest<ConnectorEvent>(request);
        return response;
    }

}
