using Mindr.Core.Models.Connector;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindr.WebUI.Services.ApiClients;

public interface IHttpConnectorEventClient
{
    Task<HttpResponseMessage?> GetAll();
    Task<HttpResponseMessage?> Create(ConnectorEvent @event);
    Task<HttpResponseMessage?> Update(Guid eventId, ConnectorEvent @event);
    Task<HttpResponseMessage?> Delete(Guid eventId);
}