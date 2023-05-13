using Mindr.Api.Models.Connectors;
using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Connector;

namespace Mindr.Api.Services.Connectors
{
    public interface IConnectorManager
    {
        Task<Connector> GetById(string userId, Guid id);
        Task<IEnumerable<ConnectorBriefDTO>> GetAll(string userId);
        Task<IEnumerable<ConnectorBriefDTO>> GetAllByQuery(string userId, string? query);
        Task<IEnumerable<ConnectorBriefDTO>> GetAllByEventId(string userId, string eventId);
        Task<Connector> Create(string userId, ConnectorOnCreate input);
        Task<ConnectorOverviewDTO> GetOverview(string userId, Guid connectorId);
        Task<ConnectorOverviewDTO> UpdateOverview(string userId, Guid id, ConnectorOverviewDTO input);
        Task<IEnumerable<HttpItem>> UpdateHttpItems(string userId, Guid id, IEnumerable<HttpItem> input);
        Task<Connector> Delete(string userId, Guid id);
    }
}