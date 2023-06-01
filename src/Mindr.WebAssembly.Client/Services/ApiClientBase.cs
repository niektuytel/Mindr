using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Mindr.WebAssembly.Client.Models.Options;
using Mindr.WebAssembly.Client.Handlers;
using System.Text;
using System.Text.Json.Serialization;
using Mindr.Domain;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Services;

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
        var content = await response.Content.ReadAsStringAsync();

        return new JsonResponse<T>(response.StatusCode, content);
    }

}
