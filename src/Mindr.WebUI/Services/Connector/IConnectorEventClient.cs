using Mindr.Core.Models.Connector;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mindr.WebUI.Services.Connector;

public interface IConnectorEventClient
{
    Task<HttpResponseMessage?> Delete(Guid eventId);
    Task<HttpResponseMessage?> Upsert(ConnectorEvent @event);
}