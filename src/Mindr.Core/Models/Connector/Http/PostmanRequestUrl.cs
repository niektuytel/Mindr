using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanRequestUrl
    {

        [JsonProperty("raw")]
        public string Raw { get; set; }

        [JsonProperty("protocol")]
        public string Protocol { get; set; }

        // TODO: Use No-SQL database (better for searching as this will been re-used?)
        [NotMapped]
        [JsonProperty("host")]
        public string[] Host { get; set; }

        // TODO: Use No-SQL database (better for searching as this will been re-used?)
        [NotMapped]
        [JsonProperty("path")]
        public string[] Path { get; set; }

        [JsonProperty("query")]
        public IEnumerable<HttpRequestUrlQuery> Query { get; set; }

    }
}