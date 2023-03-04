using Newtonsoft.Json;

namespace Mindr.Core.Models.Connector.Http
{
    public class PostmanCollectionInfo
    {

        [JsonProperty("_postman_id")]
        public string PostmanId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("schema")]
        public string Schema { get; set; }

        [JsonProperty("_exporter_id")]
        public string ExporterId { get; set; }
    }
}
