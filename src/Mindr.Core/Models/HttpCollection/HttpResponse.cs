
using System.Collections.Generic;
using System.Net.Http;

namespace Mindr.Core.Models.HttpCollection
{
    public class HttpResponse: PostmanResponse
    {
        public IEnumerable<HttpVariable>? Variables { get; set; } = null;
    }
}

