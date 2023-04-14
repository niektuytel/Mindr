using System.ComponentModel.DataAnnotations;
using System;

namespace Mindr.HttpRunner.Models
{
    public class HttpCollectionInfo : PostmanCollectionInfo
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

    }
}
