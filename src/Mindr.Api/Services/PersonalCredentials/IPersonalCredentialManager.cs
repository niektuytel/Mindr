using Mindr.Api.Models.Connectors;

using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.PersonalCredential;

namespace Mindr.Api.Services.PersonalCredentials
{
    public interface IPersonalCredentialManager
    {
        Task<IEnumerable<PersonalCredential>> GetAllById(string userId, Guid id);
        Task<IEnumerable<PersonalCredential>> GetAll(string userId);
        Task<PersonalCredential> Create(string userId, PersonalCredentialDTO input);
        Task<PersonalCredential> Update(string userId, Guid id, PersonalCredentialDTO input);
        Task<PersonalCredential> Delete(string userId, Guid id);
    }
}