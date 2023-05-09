﻿using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;

using Mindr.WebAssembly.Client.Services;
using Mindr.Domain.Models.DTO.Connector;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mindr.WebAssembly.Client.Pages.Connectors.Views;

public partial class ConnectorView : FluentComponentBase
{
    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; }

    [Parameter, EditorRequired]
    public string? ConnectorId { get; set; }

    [Parameter, EditorRequired]
    public string? NavName { get; set; }
    private string? ErrorMessage { get; set; }

    private bool IsLoading = false;

    private ConnectorOverviewDTO? ConnectorInfo { get; set; } = null;

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
        (ConnectorInfo, ErrorMessage) = response.AsTuple();
        if(!string.IsNullOrEmpty(ErrorMessage))
        {
            base.StateHasChanged();
        }

        IsLoading = false;
        base.StateHasChanged();
    }
}
