namespace Mindr.WebUI.Models;



public class HttpRequest: PostmanRequest
{
    public IEnumerable<HttpVariable>? Variables { get; set; } = null;
}
