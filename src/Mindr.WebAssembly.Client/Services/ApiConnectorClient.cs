using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Mindr.WebAssembly.Client.Models.Options;
using Mindr.WebAssembly.Client.Handlers;
using System.Text;
using System.Text.Json.Serialization;
using Mindr.Domain;
using System.Text.Json;

using Mindr.Domain.HttpRunner.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Services;

public class ApiConnectorClient : ApiClientBase, IApiConnectorClient
{
    private static readonly string Path = "api/connector";
    private static readonly string HttpClientName = nameof(AuthorizedMindrApiHandler);

    public ApiConnectorClient(IJSRuntime JSRuntime, IHttpClientFactory factory) 
        : base(JSRuntime, factory.CreateClient(HttpClientName))
    {
    }

    public async Task<JsonResponse<Connector>> Get(string connectorId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Path}/personal/{connectorId}");
        
        var response = await ApiRequest<Connector>(request);
        return response;
    }

    public async Task<JsonResponse<ICollection<ConnectorBriefDTO>>> GetAll(string query = "", string eventId = "")
    {
        if (!string.IsNullOrEmpty(query))
        {
            query = $"&query={query}";
        }

        if (!string.IsNullOrEmpty(eventId))
        {
            eventId = $"&eventId={eventId}";
        }

        var request = new HttpRequestMessage(HttpMethod.Get, $"{Path}?{query}{eventId}");
        var response = await ApiRequest<ICollection<ConnectorBriefDTO>>(request);
        return response;
    }

    public async Task<JsonResponse<ConnectorOverviewDTO>> GetOverview(string connectorId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Path}/personal/{connectorId}/overview");
        
        var response = await ApiRequest<ConnectorOverviewDTO>(request);
        return response;
    }

    public async Task<JsonResponse<ConnectorOverviewDTO>> UpdateOverview(ConnectorOverviewDTO connectorOverview)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{Path}/personal/{connectorOverview.Id}/overview");
        request.Headers.Add("accept", "*/*");

        var json = JsonSerializer.Serialize(connectorOverview);
        request.Content = new StringContent(json, Encoding.UTF8, "application/content");

        var response = await ApiRequest<ConnectorOverviewDTO>(request);
        return response;
    }

    public async Task<JsonResponse<Connector>> UpdatePipeline(string connectorId, IEnumerable<HttpItem> pipeline)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{Path}/personal/{connectorId}/pipeline");
        request.Headers.Add("accept", "*/*");

        var json = JsonSerializer.Serialize(pipeline);
        request.Content = new StringContent(json, Encoding.UTF8, "application/content");

        var response = await ApiRequest<Connector>(request);
        return response;
    }

    public async Task<JsonResponse<Connector>> Create(Connector connector)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{Path}/personal");
        request.Headers.Add("accept", "*/*");

        var content = JsonSerializer.Serialize(connector);
        request.Content = new StringContent(content, Encoding.UTF8, "application/content");

        var response = await ApiRequest<Connector>(request);
        return response;
    }

    public async Task<JsonResponse<Connector>> Delete(string connectorId)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{Path}/personal/{connectorId}");

        var response = await ApiRequest<Connector>(request);
        return response;
    }

}
