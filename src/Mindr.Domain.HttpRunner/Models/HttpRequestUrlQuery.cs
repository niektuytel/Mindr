using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpRequestUrlQuery : PostmanRequestUrlQuery
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

