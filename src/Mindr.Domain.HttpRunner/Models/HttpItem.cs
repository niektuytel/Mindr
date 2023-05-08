
using Microsoft.AspNetCore.Components;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;

namespace Mindr.HttpRunner.Models
{
    public class HttpItem : PostmanItem
    {
        [Key]
        //[ConcurrencyCheck]
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsLoading { get; set; } = false;

        [NotMapped]
        public HttpResponseMessage Result { get; set; } = null;
    }
}
