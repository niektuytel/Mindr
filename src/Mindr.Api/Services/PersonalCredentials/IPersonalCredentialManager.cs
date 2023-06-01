using Mindr.Api.Models.Connectors;

using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.Personal;

namespace Mindr.Api.Services.PersonalCredentials
{
    public interface IPersonalCredentialManager
    {
        Task<IEnumerable<PersonalCredential>> GetAllById(string userId, Guid id);
        Task<IEnumerable<PersonalCredential>> GetAll(string userId);
        Task<PersonalCredential> Upsert(string userId, PersonalCredential input);
        Task<PersonalCredential> Update(string userId, Guid id, PersonalCredentialDTO input);
        Task<PersonalCredential> Delete(string userId, Guid id);
    }
}