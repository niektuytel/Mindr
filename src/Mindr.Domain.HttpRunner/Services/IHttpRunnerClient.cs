using Mindr.Domain.HttpRunner.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindr.Domain.HttpRunner.Services
{
    public interface IHttpRunnerClient
    {
        Task<HttpItem> SendAsync(HttpItem item);
        Task<List<HttpItem>> SendAsync(List<HttpItem> pipeline);
    }
}