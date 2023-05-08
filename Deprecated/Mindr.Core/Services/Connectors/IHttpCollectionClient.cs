using Mindr.Core.Models.Connector.Http;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindr.Core.Services.Connectors
{
    public interface IHttpCollectionClient
    {
        Task<HttpItem> SendAsync(HttpItem item);

        Task<List<HttpItem>> SendAsync(List<HttpItem> pipeline);
        Task SendAsync(Models.Connector.Connector connector);
    }
}