using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanRequestUrl
    {

        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("protocol")]
        public string Protocol { get; set; }

        [JsonPropertyName("host")]
        public string Host { get; set; } = "";

        [NotMapped]
        [JsonIgnore]
        public string[] Hosts
        {
            get => Host?.Split(".")?.ToArray();
            set => Host = string.Join(".", value);
        }

        [JsonPropertyName("path")]
        public string Path { get; set; } = "";

        [NotMapped]
        [JsonIgnore]
        public string[] Paths
        {
            get => Path?.Split("/")?.ToArray();
            set => Path = string.Join("/", value);
        }

        [JsonPropertyName("query")]
        public IEnumerable<HttpRequestUrlQuery> Query { get; set; }

    }
}