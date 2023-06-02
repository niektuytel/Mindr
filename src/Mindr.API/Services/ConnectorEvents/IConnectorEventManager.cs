using Mindr.Api.Models.ConnectorEvents;
using Mindr.Domain.Models.DTO.Connector;

namespace Mindr.Api.Services.ConnectorEvents;

public interface IConnectorEventManager
{
    Task<ConnectorEvent> Get(string userId, Guid id);
    Task<IEnumerable<ConnectorEvent>> GetAll(string? userId);
    Task<IEnumerable<ConnectorEvent>> GetAllByEventId(string? userId, string? eventId);
    Task<IEnumerable<ConnectorEvent>> GetAllByConnectorId(string? userId, Guid? connectorId);
    Task<IEnumerable<ConnectorEvent>> GetAllByQuery(string userId, string query);
    Task<ConnectorEvent> Upsert(string userId, ConnectorEvent input);
    Task<ConnectorEvent> Update(string userId, Guid id, ConnectorEventOnUpdate input);
    Task<ConnectorEvent> Insert(string userId, ConnectorEventOnCreate input);
    Task<ConnectorEvent> Delete(string userId, Guid id);
}