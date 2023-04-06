using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;

namespace Mindr.Api.Services
{
    public interface IConnectorClient
    {
        Task<Connector?> Get(Guid connectorId);
        Task<Connector?> GetOverview(Guid connectorId);
        Task UpdateOverview(string userId, Connector payload);
        Task UpdateHttpItems(string userId, Guid connectorId, IEnumerable<HttpItem> payload);



        Task<IEnumerable<Connector>> GetAll(string userId, string? eventId, string? query);
        Task<IEnumerable<Connector>> GetAllBriefly(string userId);
        Task<Connector> Insert(string userId, Connector payload);
        Task Delete(string userId, Guid id);
    }
}