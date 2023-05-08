using Force.DeepCloner;
using Mindr.HttpRunner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Mindr.HttpRunner.Services
{
    public class HttpRunnerClient : IHttpRunnerClient
    {
        private readonly HttpClient _client;
        private readonly IHttpRunnerFactory _factory;

        public HttpRunnerClient(IHttpClientFactory client, IHttpRunnerFactory factory)
        {
            _client = client.CreateClient(nameof(HttpRunnerClient));
            _factory = factory;
        }

        public async Task<HttpItem> SendAsync(HttpItem item)
        {
            var httpMessage = _factory.CreateHttpMessage(item.Request);

            item.IsLoading = true;
            item.Result = null;
            item.Result = await _client.SendAsync(httpMessage);
            item.IsLoading = false;

            return item;
        }

        public async Task<List<HttpItem>> SendAsync(List<HttpItem> pipeline)
        {
            pipeline ??= new List<HttpItem>();

            for (int i = 0; i < pipeline.Count; i++)
            {
                pipeline[i] = await SendAsync(pipeline[i]);

                // break pipeline if failed request sended
                if (!pipeline[i].Result.IsSuccessStatusCode)
                {
                    pipeline.ForEach(item => item.IsLoading = false);
                    break;
                }
            }

            return pipeline;
        }

    }
}
