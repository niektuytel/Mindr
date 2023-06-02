using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mindr.WebAssembly.Client.Services;

public interface IApiConnectorEventClient
{
    Task<JsonResponse<ConnectorEvent>> Get(string eventId);

    Task<JsonResponse<IEnumerable<ConnectorEvent>>> GetAll(string query = "", string eventId = "");

    Task<JsonResponse<ConnectorEvent>> Insert(ConnectorEvent connectorEvent);

    Task<JsonResponse<ConnectorEvent>> Update(Guid eventid, ConnectorEvent connectorEvent);

    Task<JsonResponse<ConnectorEvent>> Delete(Guid eventid);

}