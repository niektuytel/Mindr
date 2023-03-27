using Force.DeepCloner;
using Mindr.Core.Extensions;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Mindr.Core.Services.Connectors
{
    public class HttpCollectionClient : IHttpCollectionClient
    {
        private readonly HttpClient _client;
        private readonly IHttpCollectionFactory _factory;

        public HttpCollectionClient(IHttpClientFactory client, IHttpCollectionFactory factory)
        {
            _client = client.CreateClient(nameof(HttpCollectionClient));
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

        public async Task SendAsync(Connector connector)
        {
            var pipeline = connector.Pipeline.ToList();
            pipeline = await SendAsync(pipeline);

            // safe current outputted response
            throw new NotImplementedException();

        }
    }
}
