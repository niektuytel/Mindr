﻿using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core.Models;
using Mindr.WebUI.Services;

namespace Mindr.WebUI.Pages.Agenda;

public partial class AgendaPage : FluentComponentBase
{
    private static readonly DateTime InitialDate = DateTime.Now;

    [Inject]
    public IHttpAgendaClient AgendaClient { get; set; } = default!;

    public IEnumerable<CalendarDay> Days { get; set; } = default!;

    // TODO: Agenda remove underflow and overflow of other months.

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
