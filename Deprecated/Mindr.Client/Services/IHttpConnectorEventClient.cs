using Mindr.Shared.Models.ConnectorEvents;

namespace Mindr.Client.Services;

public interface IHttpConnectorEventClient
{
    Task<HttpResponseMessage?> GetAll(string? eventId = null, string? query = null);
    Task<HttpResponseMessage?> Create(ConnectorEvent @event);
    Task<HttpResponseMessage?> Update(Guid eventId, ConnectorEvent @event);
    Task<HttpResponseMessage?> Delete(Guid eventId);
}