namespace Mindr.WebUI.Services
{
    public interface IConnectorClient
    {
        Task<HttpResponseMessage> GetAll(string query = "", string eventId = "");
    }
}