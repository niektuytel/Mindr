using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpCookie
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("expires")]
        public string Expires { get; set; }
    }
}