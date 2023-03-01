using Force.DeepCloner;
using Mindr.Core.Extensions;
using Mindr.Core.Interfaces;
using Mindr.Core.Models.HttpCollection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Mindr.Core.Services
{
    public class HttpCollectionClient : IHttpCollectionClient
    {
        private readonly HttpClient _client;

        public HttpCollectionClient(IHttpClientFactory client)
        {
            _client = client.CreateClient(nameof(HttpCollectionClient));
        }

        private HttpMethod GetMethod(string method)
        {
            var key = method.Trim().ToLower();

            return key switch
            {
                "get" => HttpMethod.Get,
                "post" => HttpMethod.Post,
                "put" => HttpMethod.Put,
                "patch" => HttpMethod.Patch,
                "delete" => HttpMethod.Delete,
                "options" => HttpMethod.Options,
                "head" => HttpMethod.Head,
                _ => throw new NotImplementedException($"Method {method} is not supported"),
            };
        }

        private HttpRequestMessage CreateHttpMessage(HttpRequest input)
        {
            var request = input.DeepClone();
            request.SetVariables();

            var httpMessage = new HttpRequestMessage(GetMethod(request.Method), request.Url.Raw);
            foreach (var item in request.Header)
            {
                if (item.Key == "Content-Type")
                {
                    httpMessage.Content = new StringContent(request.Body.Raw, Encoding.UTF8, item.Value);
                }
                else
                {
                    httpMessage.Headers.Add(item.Key, item.Value);
                }
            }

            return httpMessage;
        }

        public async Task<HttpItem> SendAsync(HttpItem item)
        {
            var httpMessage = CreateHttpMessage(item.Request);
            item.Result = await _client.SendAsync(httpMessage);

            return item;
        }

        public async Task<List<HttpItem>> SendAsync(List<HttpItem> pipeline)
        {
            for (int i = 0; i < pipeline.Count; i++)
            {
                pipeline[i] = await SendAsync(pipeline[i]);
            }

            return pipeline;
        }



    }
}
