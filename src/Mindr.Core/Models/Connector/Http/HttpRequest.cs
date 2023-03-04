using System;
using System.Collections.Generic;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpRequest : PostmanRequest
    {
        public IEnumerable<HttpVariable> Variables { get; set; } = null;

    }
}



