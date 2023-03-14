using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Fast.Components.FluentUI;
using Mindr.Core;
using Mindr.Core.Interfaces;
using Mindr.Core.Models;
using Mindr.Core.Models.Connector;
using Mindr.WebUI.Components;
using Mindr.WebUI.Components.Connector;
using Mindr.WebUI.Models;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Security.Principal;

namespace Mindr.WebUI.Pages;

public partial class AgendaPage: FluentComponentBase
{
    [Inject]
    public CalendarController CalendarController { get; set; } = default!;

    [Inject]
    IMicrosoftGraphProvider CalendarEventsProvider { get; set; } = default!;

    public IEnumerable<CalendarDay> Days { get; set; } = default!;

    public IEnumerable<AgendaEvent> Events { get; set; } = default!;

    public DateTime SelectedDate { get; set; } = DateTime.Now;

    //public AgendaEvent? SelectedEvent { get; set; } = null;


    protected override async Task OnInitializedAsync()
    {
        Days = CalendarController.BuildMonthCalendarDays(SelectedDate.Year, SelectedDate.Month);
        // TODO: Events = await CalendarEventsProvider.GetEventsInMonthAsync(SelectedDate.Year, SelectedDate.Month);
    }

    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {

        }
    }
    public IEnumerable<AgendaEvent> GetEventsInSelectedDate()
    {
        var items = Events?.Where(item => 
            SelectedDate.Date >= item.StartDate.DateTime && 
            SelectedDate.Date <= item.EndDate.DateTime
        );

        if (items != null)
        {
            return items;
        }

        return new List<AgendaEvent>();
    }


    public async Task OnSelectDate(DateTime date)
    {
        if (SelectedDate.Month != date.Month)
        {
            Days = CalendarController.BuildMonthCalendarDays(date.Year, date.Month);
            // TODO: Events = await CalendarEventsProvider.GetEventsInMonthAsync(date.Year, date.Month);
        }

        SelectedDate = date;
        base.StateHasChanged();
    }

    public async Task OnUpsertConector(ConnectorBriefDTO item)
    {
        //if (SelectedDate.Month != date.Month)
        //{
        //    Days = CalendarController.BuildMonthCalendarDays(date.Year, date.Month);
        //    Events = await CalendarEventsProvider.GetEventsAsync(date.Year, date.Month);
        //}

        //SelectedDate = date;
        //base.StateHasChanged();
    }

}
