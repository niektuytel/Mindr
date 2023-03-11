using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Mindr.Core.Models.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mindr.Core.Services
{
    public class ConnectorHookClient : IConnectorHookClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ConnectorHookClient(IHttpClientFactory factory, IConfiguration configuration)
        {
            _httpClient = factory.CreateClient(nameof(ConnectorHookClient));
            _baseUrl = configuration["BaseUrl"];
        }

        public async Task<HttpResponseMessage> Upsert(ConnectorHook hook, string aztoken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/connectorhook");
            request.Headers.Add("accept", "*/*");
            request.Headers.Add("Authorization", $"Bearer {aztoken}");

            var json = JsonConvert.SerializeObject(hook);
            request.Content = new StringContent(json); ;
            return await _httpClient.SendAsync(request);
        }

        public async Task<HttpResponseMessage> Delete(Guid hookid, string aztoken)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/connectorhook/{hookid}");
            request.Headers.Add("Authorization", $"Bearer {aztoken}");

            return await _httpClient.SendAsync(request);
        }

    }
}
