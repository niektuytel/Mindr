using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanRequestUrl
    {

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        [JsonIgnore]
        public string Host { get; set; } = "";

        [NotMapped]
        [JsonProperty("host")]
        public string[] Hosts
        {
            get => Host?.Split(".")?.ToArray();
            set => Host = string.Join(".", value);
        }

        [JsonIgnore]
        public string Path { get; set; } = "";

        [NotMapped]
        [JsonProperty("path")]
        public string[] Paths
        {
            get => Path?.Split("/")?.ToArray();
            set => Path = string.Join("/", value);
        }

        [JsonProperty("query")]
        public IEnumerable<HttpRequestUrlQuery> Query { get; set; }

    }
}