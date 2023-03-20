namespace Mindr.WebUI.Services.Connector
{
    public interface IConnectorClient
    {
        Task<HttpResponseMessage> GetAll(string query = "", string eventId = "");
    }
}