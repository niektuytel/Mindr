using System.Text.Json;

namespace Mindr.WebAssembly.Client.Models
{
    public class ErrorMessageResponse
    {
        public ErrorMessageResponse(int code, string type, string content)
        {
            Code = code;
            Type = type;
            Content = content;
        }

        public int Code { get; }

        public string Type { get; }

        public string Content { get; }

        public T GetContent<T>()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            return JsonSerializer.Deserialize<T>(Content, options)!;
        }
    }
}