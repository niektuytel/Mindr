
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.WebUI.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Mindr.WebUI.Pages;

public partial class ConnectorPage : FluentComponentBase
{
    [Inject]
    public HttpClient Http { get; set; }

    public List<CollectionData> Collections { get; set; } = new List<CollectionData>();



}
