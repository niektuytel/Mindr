using Mindr.Core.Models.HttpCollection;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindr.Core.Interfaces
{
    public interface IHttpCollectionClient
    {
        Task<HttpItem> SendAsync(HttpItem item);

        Task<List<HttpItem>> SendAsync(List<HttpItem> pipeline);
    }
}