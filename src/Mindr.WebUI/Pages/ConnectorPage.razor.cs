
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Enums;
using Mindr.Core.Models.HttpCollection;
using Mindr.WebUI.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Mindr.WebUI.Pages;

public partial class ConnectorPage : FluentComponentBase
{
    public HttpCollection HttpCollection { get; set; } = new();
    public List<HttpItem> HttpPipeline { get; set; } = new() { _Constants.DefaultTestSample };





}
