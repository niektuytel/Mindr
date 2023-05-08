using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Mindr.Client.Models.Options;
using Mindr.Shared.Models.ConnectorEvents;
using Mindr.Client.Handlers;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Mindr.Shared.Models.Connectors;
using Microsoft.JSInterop;
using Dantooine.WebAssembly.Client.Models;
using System.Collections.Generic;
using System.Text.Json;

namespace Mindr.Client.Services;

public class ApiConnectorEventClient : ApiClientBase, IApiConnectorEventClient
{
    private static readonly string Path = "api/connectorevent/personal";
    private static readonly string HttpClientName = "authorizedClient";

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

    public async Task<JsonResponse<ConnectorEvent>> Create(ConnectorEvent connectorEvent)
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
