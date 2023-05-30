
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpItem : PostmanItem
    {
        public HttpItem()
        { }

        public HttpItem(Guid id, HttpItem entity)
        {
            Id = id;
            IsLoading = entity.IsLoading;
            Items = entity.Items?.Select(item => 
                id == entity.Id ? new HttpItem(item.Id, item) : new HttpItem(Guid.NewGuid(), item)
            ).ToArray();
            Name = entity.Name;
            Description = entity.Description;
            Request = id == entity.Id ? new HttpRequest(entity.Request.Id, entity.Request) : new HttpRequest(Guid.NewGuid(), entity.Request);
            Response = entity.Response?.Select(
                item => id == entity.Id ? new HttpResponse(item.Id, item) : new HttpResponse(Guid.NewGuid(), item)
            ).ToArray();
            Result = entity.Result;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsLoading { get; set; } = false;

        [NotMapped]
        public HttpResponseMessage Result { get; set; } = null;
    }
}
