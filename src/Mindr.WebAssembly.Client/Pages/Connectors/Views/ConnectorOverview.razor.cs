using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Shared.Models.Connectors;
using Mindr.Client.Pages.Connectors.Components;
using Mindr.Client.Services;
using System.Threading.Tasks;

namespace Mindr.Client.Pages.Connectors.Views
{
    public partial class ConnectorOverview : FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IApiConnectorClient ConnectorClient { get; set; }

        [Parameter, EditorRequired]
        public ConnectorOverviewDTO Overview { get; set; } = null!;

        private string? ErrorMessage { get; set; }
        private bool DataHasChanged = false;

        public ConfirmDialog RemoveItemDialog = default!;

        public async Task OnSave()
        {
            var response = await ConnectorClient.UpdateOverview(Overview);
            (_, ErrorMessage) = response.AsTuple();
            base.StateHasChanged();
        }

        public async Task OnRemove()
        {
            var response = await ConnectorClient.Delete(Overview.Id.ToString());
            (_, ErrorMessage) = response.AsTuple();
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                base.StateHasChanged();
                return;
            }

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
