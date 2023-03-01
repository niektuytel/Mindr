
using System.Net.Http;

namespace Mindr.Core.Models.HttpCollection
{
    public class HttpItem: PostmanItem
    {
        public HttpResponseMessage? Result { get; set; } = null;
    }
}
