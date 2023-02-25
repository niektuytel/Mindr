namespace Mindr.WebUI.Models;

public class PostmanRequest
{
    public string method { get; set; }
    public HttpHeader[] header { get; set; }
    public HttpBody body { get; set; }
    public HttpRequestUrl url { get; set; }

}
