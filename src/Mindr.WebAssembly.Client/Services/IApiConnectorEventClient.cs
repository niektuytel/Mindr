using Dantooine.WebAssembly.Client.Models;
using Mindr.Shared.Models.ConnectorEvents;
using Mindr.Shared.Models.Connectors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mindr.Client.Services;

public interface IApiConnectorEventClient
{
    Task<JsonResponse<ConnectorEvent>> Get(string eventId);

    Task<JsonResponse<IEnumerable<ConnectorEvent>>> GetAll(string query = "", string eventId = "");

    Task<JsonResponse<ConnectorEvent>> Create(ConnectorEvent connectorEvent);

    Task<JsonResponse<ConnectorEvent>> Update(Guid eventid, ConnectorEvent connectorEvent);

    Task<JsonResponse<ConnectorEvent>> Delete(Guid eventid);

}