using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.Personal;

namespace Mindr.Api.Services.PersonalCredentials
{
    public interface IPersonalCredentialValidator
    {
        void ThrowOnInvalidUserId(string userId);
        void ThrowOnNullPersonalCredential(Guid? id, PersonalCredential? entity);
    }
}