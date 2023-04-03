using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.WebUI.Services.ApiClients;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Mindr.WebUI.Pages
{
    public partial class ConnectorEventsPage : FluentComponentBase
    {
        [Parameter]
        public string? EventId { get; set; }

    }
}
