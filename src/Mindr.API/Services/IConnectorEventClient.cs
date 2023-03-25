using Mindr.Core.Models.Connector;

namespace Mindr.API.Services;

public interface IConnectorEventClient
{
    Task<IEnumerable<ConnectorEvent>> GetAll(string userId);
    Task Upsert(ConnectorEvent @event);
    Task Delete(Guid id, string userId);
}