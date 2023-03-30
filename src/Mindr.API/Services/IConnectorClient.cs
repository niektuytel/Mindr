using Mindr.Core.Models.Connector;

namespace Mindr.Api.Services
{
    public interface IConnectorClient
    {
        Task Delete(string userId, Guid id);
        Task<IEnumerable<Connector>> GetAll(string userId, string? eventId, string? query);
        Task<IEnumerable<Connector>> GetAllBriefly(string userId);

        Task<Connector> Insert(string userId, Connector payload);
    }
}