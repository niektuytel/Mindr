using Mindr.Shared.Models.ConnectorEvents;

namespace Mindr.Api.Services.ConnectorEvents
{
    public interface IConnectorEventDriver
    {
        Task<string?> ProcessConnectorEventAsync(ConnectorEvent entity);
    }
}