using Mindr.Core.Models.Connector;

namespace Mindr.Api.Services
{
    public interface IConnectorClient
    {
        Task<IEnumerable<Connector>> GetAll(string userId, string? eventId, string? query);
    }
}