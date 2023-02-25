namespace Mindr.WebUI.Models;



public class HttpRequest: PostmanRequest
{
    public List<HttpRequestVariable> Variables { get; set; }
}
