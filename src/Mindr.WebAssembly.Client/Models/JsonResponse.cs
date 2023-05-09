namespace Mindr.WebAssembly.Client.Models;

public class JsonResponse<TData>
{
    public JsonResponse(TData data)
    {
        Data = data;
    }

    public JsonResponse(string error)
    {
        Error = error;
    }

    public TData Data { get; set; }

    public string Error { get; set; } = "";


    public (TData data, string error) AsTuple()
    {
        return (Data, Error);
    }
}
