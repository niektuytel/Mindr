using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.Domain.HttpRunner.Models
{
    public class HttpCollectionInfo : PostmanCollectionInfo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}
