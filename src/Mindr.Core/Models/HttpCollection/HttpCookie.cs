using Newtonsoft.Json;

namespace Mindr.Core.Models.HttpCollection
{
    public class HttpCookie
    {
        [JsonProperty("expires")]
        public string Expires { get; set; }
    }
}