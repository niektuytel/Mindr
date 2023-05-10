using Microsoft.AspNetCore.Identity;
using Mindr.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mindr.Domain.OpenId
{
    public class UserRelation
    {
        [Key]
        public Guid Id { get; set; }

        public string ApplicationUserId { get; set; }

        public OpenIdServer OpenIdProvider { get; set; }
    }
}
