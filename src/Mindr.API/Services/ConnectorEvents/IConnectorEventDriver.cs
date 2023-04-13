using Mindr.Core.Models.Connector;

namespace Mindr.Api.Services.ConnectorEvents
{
    public interface IConnectorEventDriver
    {
        Task<string?> ProcessConnectorEventAsync(ConnectorEvent entity);
    }
}