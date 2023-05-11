using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Domain.Models;
using Mindr.WebAssembly.Client.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Linq;
using Mindr.WebAssembly.Client.Models;
using Mindr.WebAssembly.Client.Pages.Connectors.Components;
using Mindr.WebAssembly.Client.Pages.Agenda.Components;

namespace Mindr.WebAssembly.Client.Pages.Agenda;

public partial class AgendaPage : FluentComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }

    public GoogleCalendarDialog AddGoogleCalendarDialog = default!;

    public string RedirectUri => $"{NavigationManager.BaseUri[..^1]}/agenda";

    protected override async Task OnInitializedAsync()
    {
        //await OnSelectMonth(SelectedDate);
    }

    public async Task HandleAddGoogleCalendarDialogOpen()
    {

        await AddGoogleCalendarDialog.HandleDialogOpen();
    }

    public void HandleAddGoogleCalendarDialogClose()
    {
        AddGoogleCalendarDialog.HandleDialogClose();
    }

    public void HandleAddGoogleCalendarDialogDismiss(DialogEventArgs args)
    {
        if (args is not null && args.Reason is not null && args.Reason == "dismiss")
        {
            AddGoogleCalendarDialog.HandleDialogClose();
        }
    }

    #region Deprecated
    private static readonly DateTime InitialDate = DateTime.Now;

    //[Inject]
    //public IHttpAgendaClient AgendaClient { get; set; } = default!;

    public IEnumerable<CalendarDay> Days { get; set; } = default!;

    public IEnumerable<AgendaEvent>? Events { get; set; } = default!;

    public IEnumerable<AgendaEvent>? CurrentEvents { get; set; } = default!;

    public DateTime SelectedDate { get; set; } = InitialDate;

    public async Task OnSelectMonth(DateTime date)
    {
        if (SelectedDate.Month != date.Month || date == InitialDate)
        {
            //Days = AgendaClient.GetMonthCalendarDays(date);
            //Events = await AgendaClient.GetEventsOnMonth(date);
        }

        SelectedDate = date;
        base.StateHasChanged();
    }

    public async Task<IEnumerable<AgendaEvent>?> HandleOnSelect(DateTime date)
    {
        CurrentEvents = Events?.Where(item =>
            item != null &&
            date.Date >= item.StartDate.DateTime.Date &&
            date.Date <= item.EndDate.DateTime.Date
        );

        SelectedDate = date;
        base.StateHasChanged();
        return CurrentEvents;
    }

    #endregion
}
