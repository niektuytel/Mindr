using System.Text.Json.Serialization;

namespace Mindr.Domain.HttpRunner.Models
{
    public class PostmanCollectionInfo
    {

        [JsonPropertyName("_postman_id")]
        public string PostmanId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("schema")]
        public string Schema { get; set; }

        [JsonPropertyName("_exporter_id")]
        public string ExporterId { get; set; }
    }
}
