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

        [JsonIgnore]
        public string Host { get; set; } = "";

        [NotMapped]
        [JsonPropertyName("host")]
        public string[] Hosts
        {
            get => Host?.Split(".")?.ToArray();
            set => Host = string.Join(".", value);
        }

        [JsonIgnore]
        public string Path { get; set; } = "";

        [NotMapped]
        [JsonPropertyName("path")]
        public string[] Paths
        {
            get => Path?.Split("/")?.ToArray();
            set => Path = string.Join("/", value);
        }

        [JsonPropertyName("query")]
        public IEnumerable<HttpRequestUrlQuery> Query { get; set; }

    }
}