
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpResponse : PostmanResponse
    {
        public HttpResponse()
        { }

        public HttpResponse(Guid id, HttpResponse entity)
        {
            Id = id;
            Name = entity.Name;
            OriginalRequest = id == entity.Id ? new HttpRequest(entity.OriginalRequest.Id, entity.OriginalRequest) : new HttpRequest(Guid.NewGuid(), entity.OriginalRequest);
            Status = entity.Status;
            Code = entity.Code;
            PostmanPreviewLanguage = entity.PostmanPreviewLanguage;
            Header = entity.Header.Select(item =>
                id == entity.Id ? new HttpHeader(item.Id, item) : new HttpHeader(Guid.NewGuid(), item)
            ).ToArray();
            Cookie = entity.Cookie.Select(item =>
                id == entity.Id ? new HttpCookie(item.Id, item) : new HttpCookie(Guid.NewGuid(), item)
            ).ToArray();
            Body = entity.Body;
            Variables = entity.Variables.Select(item =>
                id == entity.Id ? new HttpVariable(item.Id, item) : new HttpVariable(Guid.NewGuid(), item)
            ).ToArray();
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public IEnumerable<HttpVariable> Variables { get; set; } = new List<HttpVariable>();
    }
}

