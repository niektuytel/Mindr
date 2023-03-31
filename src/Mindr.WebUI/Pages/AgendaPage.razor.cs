using DutchGrit.Afas.Refinery;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Graph;
using Microsoft.Graph.TermStore;
using Mindr.Core.Models;
using Mindr.Core.Models.Connector;
using Mindr.WebUI.Components;
using Mindr.WebUI.Handlers;
using Mindr.WebUI.Models;
using Mindr.WebUI.Services;
using Mindr.WebUI.Services.ApiClients;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Security.Principal;

namespace Mindr.WebUI.Pages;

public partial class AgendaPage: FluentComponentBase
{
    private static readonly DateTime InitialDate = DateTime.Now;

    [Inject]
    public IHttpAgendaHttpClient AgendaClient { get; set; } = default!;

    public IEnumerable<CalendarDay> Days { get; set; } = default!;

    public IEnumerable<AgendaEvent>? Events { get; set; } = default!;

    public IEnumerable<AgendaEvent>? CurrentEvents { get; set; } = default!;

    public DateTime SelectedDate { get; set; } = InitialDate;

    protected override async Task OnInitializedAsync()
    {
        await OnSelectMonth(SelectedDate);
    }

    public async Task OnSelectMonth(DateTime date)
    {
        if (SelectedDate.Month != date.Month || date == InitialDate)
        {
            Days = AgendaClient.GetMonthCalendarDays(date);
            Events = await AgendaClient.GetEventsOnMonth(date);
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
}
