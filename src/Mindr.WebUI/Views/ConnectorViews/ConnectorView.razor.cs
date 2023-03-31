using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.WebUI.Services.ApiClients;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Mindr.WebUI.Views.ConnectorViews
{
    public partial class ConnectorView : FluentComponentBase
    {
        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        [Parameter, EditorRequired]
        public string? ConnectorId { get; set; }

        [Parameter, EditorRequired]
        public string? NavName { get; set; }

        private bool IsLoading = false;
        
        private Connector? ConnectorInfo { get; set; } = null;
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await OnConnectorLoad();
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task OnConnectorLoad()
        {
            if (string.IsNullOrEmpty(ConnectorId))
            {
                return;
            }

            IsLoading = true;

            var response = await ConnectorClient.GetBriefly(ConnectorId);
            if (response == null)
            {
                // Failed request
                throw new NotImplementedException();
            }

            var json = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(json))
            {
                ConnectorInfo = JsonConvert.DeserializeObject<Connector>(json);
            }

            IsLoading = false;
            base.StateHasChanged();
        }
    }
}
