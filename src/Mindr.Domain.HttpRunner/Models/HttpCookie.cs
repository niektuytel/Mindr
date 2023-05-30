using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpCookie
    {
        public HttpCookie()
        { }

        public HttpCookie(Guid id, HttpCookie entity)
        {
            Id = id;
            Expires = entity.Expires;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("expires")]
        public string Expires { get; set; }
    }
}