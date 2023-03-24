using Mindr.Core.Models.Connector;

namespace Mindr.API.Services
{
    public interface IConnectorEventClient
    {
        Task Upsert(ConnectorEvent @event);
    }
}