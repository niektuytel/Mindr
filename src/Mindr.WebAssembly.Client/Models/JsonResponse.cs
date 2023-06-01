using System.Net;
using System.Text.Json;

namespace Mindr.WebAssembly.Client.Models;

public class JsonResponse<TJSON>
{
    public JsonResponse(HttpStatusCode httpStatusCode, string content)
    {
        StatusCode = httpStatusCode;
        Content = content;
    }

    public HttpStatusCode StatusCode { get; set; }

    public string Content { get; set; }

    public bool IsError()
    {
        return StatusCode != HttpStatusCode.OK;
    }

    public bool IsSuccessful()
    {
        return StatusCode == HttpStatusCode.OK;
    }

    public string GetContent()
    {
        return Content;
    }

    public T GetContent<T>()
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };

        return JsonSerializer.Deserialize<T>(Content, options)!;
    }
}
