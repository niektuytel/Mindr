using Azure;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models.Connectors;
using Mindr.WebUI.Pages.Connectors.Components;
using Mindr.WebUI.Services;

namespace Mindr.WebUI.Pages.Connectors.Views
{
    public partial class ConnectorOverview : FluentComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        [Parameter, EditorRequired]
        public Connector Overview { get; set; } = null!;

        private string? ErrorMessage { get; set; }
        private bool DataHasChanged = false;

        public ConfirmDialog RemoveItemDialog = default!;

        public async Task OnSave()
        {
            var response = await ConnectorClient.UpdateOverview(Overview);
            if (response == null)
            {
                // TODO: should be fixed with refresh token

                ErrorMessage = $"Login session expired, Please login again";
                base.StateHasChanged();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    ErrorMessage = content;
                    base.StateHasChanged();
                }
            }

            base.StateHasChanged();
        }

        public async Task OnRemove()
        {
            var response = await ConnectorClient.Delete(Overview.Id.ToString());
            if (response == null)
            {
                // TODO: should be fixed with refresh token

                ErrorMessage = $"Login session expired, Please login again";
                base.StateHasChanged();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    ErrorMessage = content;
                    base.StateHasChanged();
                }
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
