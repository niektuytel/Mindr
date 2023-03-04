
using System.Collections.Generic;
using System.Net.Http;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpResponse : PostmanResponse
    {
        public IEnumerable<HttpVariable> Variables { get; set; } = null;
    }
}

