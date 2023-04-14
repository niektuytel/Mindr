using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.HttpRunner.Models
{
    public class HttpCookie
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("expires")]
        public string Expires { get; set; }
    }
}