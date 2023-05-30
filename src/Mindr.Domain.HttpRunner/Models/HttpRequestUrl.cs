using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpRequestUrl : PostmanRequestUrl
    {
        public HttpRequestUrl()
        { }

        public HttpRequestUrl(Guid id, HttpRequestUrl entity)
        {
            Id = id;
            Raw = entity.Raw;
            Protocol = entity.Protocol;
            Host = entity.Host;
            Path = entity.Path;
            Query = entity.Query?.Select(item =>
                id == entity.Id ? new HttpRequestUrlQuery(item.Id, item) : new HttpRequestUrlQuery(Guid.NewGuid(), item)
            ).ToArray();
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

