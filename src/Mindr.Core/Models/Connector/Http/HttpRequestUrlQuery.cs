using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpRequestUrlQuery : PostmanRequestUrlQuery
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}

