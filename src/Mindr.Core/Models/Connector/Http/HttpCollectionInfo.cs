using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Core.Models.Connector.Http
{
    public class HttpCollectionInfo : PostmanCollectionInfo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}
