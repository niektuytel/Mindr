using Mindr.Domain.HttpRunner.Models;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

namespace Mindr.Domain.HttpRunner.Services
{
    public interface IHttpRunnerFactory
    {
        HttpRequestMessage CreateHttpMessage(HttpRequest input);
        HttpItem PrepareHttpItem(HttpItem item, IEnumerable<HttpItem> httpPipeline, HttpCollection postmanCollection);
    }
}