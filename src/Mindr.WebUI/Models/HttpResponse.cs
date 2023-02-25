namespace Mindr.WebUI.Models;



public class HttpResponse
{
    public string name { get; set; }
    public HttpRequest originalRequest { get; set; }
    public string status { get; set; }
    public int code { get; set; }
    public string _postman_previewlanguage { get; set; }
    public object header { get; set; }
    public object[] cookie { get; set; }
    public string body { get; set; }
}
