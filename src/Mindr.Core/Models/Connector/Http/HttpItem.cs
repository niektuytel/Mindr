
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Http;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpItem : PostmanItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsLoading { get; set; } = false;

        public HttpResponseMessage Result { get; set; } = null;
    }
}
