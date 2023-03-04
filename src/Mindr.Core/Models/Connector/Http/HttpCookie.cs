using Newtonsoft.Json;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpCookie
    {
        [JsonProperty("expires")]
        public string Expires { get; set; }
    }
}