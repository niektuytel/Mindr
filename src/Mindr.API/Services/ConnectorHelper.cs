using Mindr.Core.Models.Connector.Http;
using HttpRequest = Mindr.Core.Models.Connector.Http.HttpRequest;

namespace Mindr.Api.Services
{
    public class ConnectorHelper
    {

        public async Task<HttpItem[]> PrepareHttpItems(HttpItem[] httpItems)
        {
            for (int i = 0; i < httpItems.Length; i++)
            {
                httpItems[i] = await PrepareHttpItem(httpItems[i]);
            }

            return httpItems;
        }

        public async Task<HttpItem> PrepareHttpItem(HttpItem httpItem)
        {
            httpItem.IsLoading = false;
            httpItem.Items = null;
            httpItem.Request = await PrepareHttpRequest(httpItem.Request);

            return httpItem;
        }

        public async Task<HttpRequest> PrepareHttpRequest(HttpRequest httpRequest)
        {


            return httpRequest;

            //public IEnumerable<HttpVariable> Variables { get; set; } = null;
            //public string Method { get; set; } = "";
            //public IEnumerable<HttpHeader> Header { get; set; } = new List<HttpHeader>();
            //public HttpBody Body { get; set; } = new HttpBody();
            //public HttpRequestUrl Url { get; set; } = new HttpRequestUrl();
        }

        public async Task<HttpRequestUrl> PrepareHttpRequestUrl(HttpRequestUrl httpRequestUrl)
        {


            return httpRequestUrl;

            //public string Raw { get; set; }
            //public string Protocol { get; set; }
            //public string Host { get; set; } = "";
            //public string[] Hosts
            //{
            //    get => Host?.Split(".")?.ToArray();
            //    set => Host = string.Join(".", value);
            //}
            //public string Path { get; set; } = "";
            //public string[] Paths
            //{
            //    get => Path?.Split("/")?.ToArray();
            //    set => Path = string.Join("/", value);
            //}
            //public IEnumerable<HttpRequestUrlQuery> Query { get; set; }

        }

    }
}
