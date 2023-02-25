namespace Mindr.WebUI.Models;



public class HttpRequestUrl
{
    public string raw { get; set; }
    public string protocol { get; set; }
    public string[] host { get; set; }
    public string[] path { get; set; }
    public HttpRequestUrlQuery[] query { get; set; }


}

