using Microsoft.EntityFrameworkCore;
using Mindr.Api.Persistence;
using Mindr.Api.Exceptions;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.Domain.Enums;
using Microsoft.Graph;

namespace Mindr.Api.Services.PersonalCredentials
{
    public class PersonalCredentialValidator : IPersonalCredentialValidator
    {
        public PersonalCredentialValidator()
        { }

        public void ThrowOnInvalidTarget(CredentialTarget target)
        {
            switch (target)
            {
                case CredentialTarget.GoogleCalendar: return;
                default: throw new HttpException<string>(HttpStatusCode.BadRequest, $"Unknown CredentialTarget:{{{nameof(target)}:'{target}'}}");
            }
        }

        public void ThrowOnInvalidUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new HttpException<string>(HttpStatusCode.BadRequest, $"Unknown {nameof(userId)}:'{userId}'");
            }
        }

        public void ThrowOnNullPersonalCredential(Guid? id, PersonalCredential? connector)
        {
            if (id == null)
            {
                throw new HttpException<string>(HttpStatusCode.BadRequest, $"Connector id '{id}' is null");
            }

            if (connector == null)
            {
                throw new HttpException<string>(HttpStatusCode.NotFound, $"Can't find connector on {nameof(id)}:'{id}' that is public");
            }
        }

    }
}