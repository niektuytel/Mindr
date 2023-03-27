namespace Mindr.WebUI.Services.Connector
{
    public interface IHttpConnectorClient
    {
        Task<HttpResponseMessage?> GetAll(string query = "", string eventId = "");
    }
}