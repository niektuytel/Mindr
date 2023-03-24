using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpCookie
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("expires")]
        public string Expires { get; set; }
    }
}