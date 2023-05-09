using Mindr.Domain.HttpRunner.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Services;

public interface IApiConnectorClient
{
    Task<JsonResponse<Connector>> Get(string connectorId);

    Task<JsonResponse<ICollection<ConnectorBriefDTO>>> GetAll(string query = "", string eventId = "");

    Task<JsonResponse<ConnectorOverviewDTO>> GetOverview(string connectorId);

    Task<JsonResponse<ConnectorOverviewDTO>> UpdateOverview(ConnectorOverviewDTO connectorOverview);

    Task<JsonResponse<Connector>> UpdatePipeline(string connectorId, IEnumerable<HttpItem> pipeline);

    Task<JsonResponse<Connector>> Create(Connector connector);

    Task<JsonResponse<Connector>> Delete(string connectorId);

}