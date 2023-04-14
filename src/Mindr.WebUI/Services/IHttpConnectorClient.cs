
using Mindr.Core.Models.Connectors;
using Mindr.HttpRunner.Models;

namespace Mindr.WebUI.Services
{
    public interface IHttpConnectorClient
    {
        Task<HttpResponseMessage?> Get(string connectorId);
        Task<HttpResponseMessage?> GetOverview(string connectorId);
        Task<HttpResponseMessage?> UpdateOverview(Connector content);
        Task<HttpResponseMessage?> UpdateHttpItems(string connectorId, IEnumerable<HttpItem> content);

        Task<HttpResponseMessage?> GetAll(string query = "", string eventId = "");
        Task<HttpResponseMessage?> Create(Connector content);
        Task<HttpResponseMessage?> Delete(string connectorId);
    }
}