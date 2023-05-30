using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpRequestUrlQuery : PostmanRequestUrlQuery
    {
        public HttpRequestUrlQuery()
        { }

        public HttpRequestUrlQuery(Guid id, HttpRequestUrlQuery entity)
        {
            Id = id;
            Key = entity.Key;
            Value = entity.Value;
            Description = entity.Description;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

