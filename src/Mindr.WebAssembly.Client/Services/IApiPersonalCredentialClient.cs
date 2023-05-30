using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Services
{
    public interface IApiPersonalCredentialClient
    {
        Task<JsonResponse<PersonalCredential>> Create(PersonalCredentialDTO credential);
    }
}