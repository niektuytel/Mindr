using Mindr.HttpRunner.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindr.HttpRunner.Services
{
    public interface IHttpRunnerClient
    {
        Task<HttpItem> SendAsync(HttpItem item);
        Task<List<HttpItem>> SendAsync(List<HttpItem> pipeline);
    }
}