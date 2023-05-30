using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpRequest : PostmanRequest
    {

        public HttpRequest()
        { }

        public HttpRequest(Guid id, HttpRequest entity)
        {
            Id = id;
            Variables = entity.Variables?.Select(item =>
                id == entity.Id ? new HttpVariable(item.Id, item) : new HttpVariable(Guid.NewGuid(), item)
            ).ToArray();
            Method = entity.Method;
            Header = entity.Header?.Select(item =>
                id == entity.Id ? new HttpHeader(item.Id, item) : new HttpHeader(Guid.NewGuid(), item)
            ).ToArray();
            Body = id == entity.Id ? new HttpBody(entity.Body.Id, entity.Body) : new HttpBody(Guid.NewGuid(), entity.Body);
            Url = id == entity.Id ? new HttpRequestUrl(entity.Body.Id, entity.Url) : new HttpRequestUrl(Guid.NewGuid(), entity.Url);
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public IEnumerable<HttpVariable> Variables { get; set; } = new List<HttpVariable>();

    }
}



