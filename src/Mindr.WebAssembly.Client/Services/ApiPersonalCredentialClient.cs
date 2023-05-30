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
using Mindr.Domain.Models.DTO.Personal;
using Mindr.Domain.Models.DTO.Calendar;
using System.Globalization;

namespace Mindr.WebAssembly.Client.Services;

public class ApiPersonalCredentialClient : ApiClientBase, IApiPersonalCredentialClient
{
    private static readonly string Path = "api/personalcredential";
    private static readonly string HttpClientName = nameof(AuthorizedMindrApiHandler);

    public ApiPersonalCredentialClient(IJSRuntime JSRuntime, IHttpClientFactory factory)
        : base(JSRuntime, factory.CreateClient(HttpClientName))
    { }

    public async Task<JsonResponse<PersonalCredential>> Create(PersonalCredentialDTO credential)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{Path}");
        request.Headers.Add("accept", "*/*");

        var content = JsonSerializer.Serialize(credential);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await ApiRequest<PersonalCredential>(request);
        return response;
    }
}
