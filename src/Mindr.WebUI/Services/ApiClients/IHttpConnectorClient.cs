using Mindr.Core.Models.Connector;

namespace Mindr.WebUI.Services.ApiClients
{
    public interface IHttpConnectorClient
    {
        Task<HttpResponseMessage?> GetAll(string query = "", string eventId = "");
        Task<HttpResponseMessage?> Create(Connector content);
        Task<HttpResponseMessage?> Delete(string connectorId);
        Task<HttpResponseMessage?> GetBriefly(string connectorId);
    }
}