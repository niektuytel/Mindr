using Mindr.Api.Models.Connectors;
using Mindr.Core.Models.Connectors;
using Mindr.HttpRunner.Models;

namespace Mindr.Api.Services.Connectors
{
    public interface IConnectorManager
    {
        Task<Connector> GetById(string userId, Guid id);
        Task<IEnumerable<ConnectorBriefDTO>> GetAllByQuery(string? query);
        Task<IEnumerable<ConnectorBriefDTO>> GetAllByEventId(string userId, string eventId);
        Task<Connector> Create(string userId, ConnectorOnCreate input);
        Task<ConnectorOverviewDTO> GetOverview(string userId, Guid connectorId);
        Task<ConnectorOverviewDTO> UpdateOverview(string userId, Guid id, ConnectorOverviewDTO input);
        Task<IEnumerable<HttpItem>> UpdateHttpItems(string userId, Guid id, IEnumerable<HttpItem> input);
        Task<Connector> Delete(string userId, Guid id);
    }
}