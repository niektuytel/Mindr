using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.HttpRunner.Models
{
    public class HttpRequestUrlQuery : PostmanRequestUrlQuery
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

