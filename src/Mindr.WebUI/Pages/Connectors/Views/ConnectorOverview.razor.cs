using Azure.Core.Pipeline;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Extensions;
using Mindr.Core.Models.Connector;
using Mindr.Core.Models.Connector.Http;
using Mindr.Core.Services.Connectors;
using Mindr.WebUI.Pages.Connectors.Components;
using Mindr.WebUI.Services;
using Newtonsoft.Json;

namespace Mindr.WebUI.Pages.Connectors.Views
{
    public partial class ConnectorOverview: FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        [Parameter, EditorRequired]
        public Connector Overview { get; set; } = null!;

        private bool DataHasChanged = false;

        public ConfirmDialog RemoveItemDialog = default!;

        public async Task OnSave()
        {
            await ConnectorClient.UpdateOverview(Overview);
            base.StateHasChanged();
        }

        public async Task OnRemove()
        {
            await ConnectorClient.Delete(Overview.Id.ToString());
            NavigationManager.NavigateTo($"/connectors");
            base.StateHasChanged();
        }

        private void OnChangeName(string value)
        {
            Overview.Name = value;
            OnDataChanged();
        }

        private void OnChangeDescription(string value)
        {
            Overview.Description = value;
            OnDataChanged();
        }

        private void OnDataChanged()
        {
            DataHasChanged = true;
            base.StateHasChanged();
        }

    }
}
