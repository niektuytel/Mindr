using Mindr.Core.Models.Connector;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindr.WebUI.Services.Connector;

public interface IHttpConnectorEventClient
{
    Task<HttpResponseMessage?> GetAll();
    Task<HttpResponseMessage?> Upsert(ConnectorEvent @event);
    Task<HttpResponseMessage?> Delete(Guid eventId);
}