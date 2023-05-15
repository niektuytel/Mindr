using Microsoft.EntityFrameworkCore;
using Mindr.Api.Persistence;
using Mindr.Api.Exceptions;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.Personal;

namespace Mindr.Api.Services.PersonalCredentials
{
    public class PersonalCredentialValidator : IPersonalCredentialValidator
    {
        public PersonalCredentialValidator()
        { }

        public void ThrowOnInvalidUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new HttpException(HttpStatusCode.BadRequest, $"Unknown {nameof(userId)}:'{userId}'");
            }
        }

        public void ThrowOnNullPersonalCredential(Guid? id, PersonalCredential? connector)
        {
            if (id == null)
            {
                throw new HttpException(HttpStatusCode.BadRequest, $"Connector id '{id}' is null");
            }

            if (connector == null)
            {
                throw new HttpException(HttpStatusCode.NotFound, $"Can't find connector on {nameof(id)}:'{id}' that is public");
            }
        }

    }
}