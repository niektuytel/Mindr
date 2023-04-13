using Mindr.Api.Models;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;

namespace Mindr.Api.Services
{
    public interface IConnectorClient
    {
        Task<ConnectorInsertResponse> Create(string userId, ConnectorInsert input);

        Task<Connector?> Get(Guid connectorId);
        Task<IEnumerable<Connector>> GetAll(string userId, string? eventId, string? query, bool asUser);
        Task<IEnumerable<Connector>> GetAllBriefly(string userId);
        Task<Connector?> GetOverview(Guid connectorId);
        Task UpdateOverview(string userId, Connector payload);
        Task UpdateHttpItems(string userId, Guid connectorId, IEnumerable<HttpItem> payload);
        Task Delete(string userId, Guid id);
    }
}