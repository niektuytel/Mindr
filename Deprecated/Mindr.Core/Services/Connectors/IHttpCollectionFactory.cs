using Mindr.Core.Models.Connector.Http;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

namespace Mindr.Core.Services.Connectors
{
    public interface IHttpCollectionFactory
    {
        HttpRequestMessage CreateHttpMessage(HttpRequest input);
        HttpItem PrepareHttpItem(HttpItem item, IEnumerable<HttpItem> httpPipeline, HttpCollection postmanCollection);
    }
}