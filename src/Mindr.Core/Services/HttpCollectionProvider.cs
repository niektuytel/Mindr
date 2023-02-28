using Mindr.Core.Models.HttpCollection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace Mindr.Core.Services
{
    internal class HttpCollectionProvider
    {
        private readonly IHttpClientFactory _client;
        
        public HttpCollectionProvider(IHttpClientFactory client) 
        {
            _client = client;
        }

        public HttpRequestMessage CreateRequest(HttpRequest request)
        {
            return null;
        }

    }
}
