using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Shared.Models.Connectors;
using Mindr.Client.Services;
using Newtonsoft.Json;

namespace Mindr.Client.Pages.Connectors.Views
{
    public partial class ConnectorView : FluentComponentBase
    {
        [Inject]
        public IHttpConnectorClient ConnectorClient { get; set; }

        [Parameter, EditorRequired]
        public string? ConnectorId { get; set; }

        [Parameter, EditorRequired]
        public string? NavName { get; set; }
        private string? ErrorMessage { get; set; }

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

            var response = await ConnectorClient.GetOverview(ConnectorId);
            if (response == null)
            {
                // TODO: should be fixed with refresh token

                ErrorMessage = $"Login session expired, Please login again";
                base.StateHasChanged();
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ConnectorInfo = JsonConvert.DeserializeObject<Connector>(content);
                }
                else
                {
                    ErrorMessage = content;
                    base.StateHasChanged();
                }
            }

            IsLoading = false;
            base.StateHasChanged();
        }
    }
}
