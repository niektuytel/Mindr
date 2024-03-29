﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Services;
using MudBlazor;
using System;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Mindr.WebAssembly.Client.Pages.Calendar.Components;

public partial class ConnectorEventDialog
{
    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; } = default!;

    [Parameter, EditorRequired]
    public ConnectorEvent Item { get; set; } = default!;

    [Inject]
    public IApiConnectorClient ConnectorClient { get; set; } = default!;

    [Inject]
    public ISnackbar Snackbar { get; set; } = default!;

    public IEnumerable<ConnectorEvent> SearchResults { get; set; } = default!;

    private bool DataHasChanged = false;

    private bool success;

    private async Task<IEnumerable<ConnectorEvent>> Search(string value, CancellationToken token)
    {
        var response = await ConnectorClient.GetAllAsEvent(query: value);
        if(response.IsError())
        {
            var error = response.GetContent();
            Snackbar.Add(error, Severity.Error);
            return Enumerable.Empty<ConnectorEvent>();
        }

        SearchResults = response.GetContent<IEnumerable<ConnectorEvent>>();
        return SearchResults;
    }

    private void OnSelect(ConnectorEvent value)
    {
        Item = value;
        DataHasChanged = true;
        base.StateHasChanged();
    }

    public void HandleOnConfirm()
    {
        MudDialog.Close(DialogResult.Ok(Item));
    }

    public void HandleOnCancel()
    {
        MudDialog.Close(DialogResult.Cancel());
    }

}
