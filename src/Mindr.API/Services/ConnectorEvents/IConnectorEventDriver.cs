

using Mindr.Domain.Models.DTO.Connector;

namespace Mindr.Api.Services.ConnectorEvents
{
    public interface IConnectorEventDriver
    {
        Task<string?> ProcessConnectorEventAsync(ConnectorEvent entity);
    }
}