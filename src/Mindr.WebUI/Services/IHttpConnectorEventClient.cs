using Mindr.Core.Models.ConnectorEvents;

namespace Mindr.WebUI.Services;

public interface IHttpConnectorEventClient
{
    Task<HttpResponseMessage?> GetAll(string? eventId = null, string? query = null);
    Task<HttpResponseMessage?> Create(ConnectorEvent @event);
    Task<HttpResponseMessage?> Update(Guid eventId, ConnectorEvent @event);
    Task<HttpResponseMessage?> Delete(Guid eventId);
}