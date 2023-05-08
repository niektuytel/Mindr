using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Mindr.Client.Models.Options;
using Mindr.Client.Handlers;
using System.Text;
using System.Text.Json.Serialization;
using Mindr.Shared;
using System.Text.Json;
using Mindr.Shared.Models.Connectors;
using Mindr.HttpRunner.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Dantooine.WebAssembly.Client.Models;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Mindr.Client.Services;

public class ApiClientBase
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _JSRuntime;

    public ApiClientBase(IJSRuntime JSRuntime, HttpClient httpClient)
    {
        _JSRuntime = JSRuntime;
        _httpClient = httpClient;
    }

    public async Task<JsonResponse<T>> ApiRequest<T>(HttpRequestMessage message) where T : class
    {
        var token = await _JSRuntime.InvokeAsync<string>("getAntiForgeryToken");
        _httpClient.DefaultRequestHeaders.Add("X-XSRF-TOKEN", token);

        var response = await _httpClient.SendAsync(message);
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var json = await JsonSerializer.DeserializeAsync<T>(content, options);
            return new JsonResponse<T>(data: json);
        }
        else
        {
            var content = await response.Content.ReadAsStringAsync();
            return new JsonResponse<T>(error: content);
        }
    }

}
