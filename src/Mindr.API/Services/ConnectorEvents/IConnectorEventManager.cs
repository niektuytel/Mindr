using Mindr.Api.Models.ConnectorEvents;
using Mindr.Domain.Models.DTO.Connector;

namespace Mindr.Api.Services.ConnectorEvents;

public interface IConnectorEventManager
{
    Task<ConnectorEvent> GetById(string userId, Guid id);
    Task<IEnumerable<ConnectorEvent>> GetAll(string? userId);
    Task<IEnumerable<ConnectorEvent>> GetAllByEventId(string? userId, string? eventId);
    Task<IEnumerable<ConnectorEvent>> GetAllByConnectorId(string? userId, Guid? connectorId);
    Task<IEnumerable<ConnectorEvent>> GetAllByQuery(string userId, string query);
    Task<ConnectorEvent> UpdateById(string userId, Guid id, ConnectorEventOnUpdate input);
    Task<ConnectorEvent> Create(string userId, ConnectorEventOnCreate input);
    Task<ConnectorEvent> DeleteById(string userId, Guid id);
}