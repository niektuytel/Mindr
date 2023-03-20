using DutchGrit.Afas.Refinery;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Graph;
using Microsoft.Graph.TermStore;
using Mindr.Core.Models;
using Mindr.Core.Models.Connector;
using Mindr.WebUI.Components;
using Mindr.WebUI.Components.Connector;
using Mindr.WebUI.Handlers;
using Mindr.WebUI.Helpers.Agenda;
using Mindr.WebUI.Models;
using Mindr.WebUI.Services;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Security.Principal;

namespace Mindr.WebUI.Pages;

public partial class AgendaPage: FluentComponentBase
{
    [Inject]
    public IAgendaHelper AgendaHelper { get; set; } = default!;

    //[Inject]
    //IMicrosoftCalendarEventsProvider CalendarEventsProvider { get; set; } = default!;

    [Inject]
    IHttpClientFactory ClientFactory { get; set; } = default!;

    [Inject]
    IConfiguration Config { get; set; } = default!;

    public IEnumerable<CalendarDay> Days { get; set; } = default!;

    public List<AgendaEvent> Events { get; set; } = default!;

    public DateTime SelectedDate { get; set; } = DateTime.Now;

    //public AgendaEvent? SelectedEvent { get; set; } = null;


    //private string ConstructGraphUrl(int year, int month)
    //{
    //    var lastDayInMonth = DateTime.DaysInMonth(year, month);
    //    return $"{BASE_URL}?$filter=start/datetime ge '{year}-{month}-01T00:00' and end/dateTime le '{year}-{month}-{lastDayInMonth}T00:00'&$select=subject,start,end";
    //}

    protected override async Task OnInitializedAsync()
    {
        Days = AgendaHelper.BuildMonthCalendarDays(SelectedDate.Year, SelectedDate.Month);

        try
        {
            var client = ClientFactory.CreateClient(nameof(AuthorizationGraphMessageHandler));

            var lastDayInMonth = DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month);

            //Events = new();
            var response = await client.GetFromJsonAsync<Core.Models.AgendaEvents>($"{Config.GetSection("MicrosoftGraph")["Version"]}/me/events?$filter=start/datetime ge '{SelectedDate.Year}-{SelectedDate.Month}-01T00:00' and end/dateTime le '{SelectedDate.Year}-{SelectedDate.Month}-{lastDayInMonth}T00:00'&$select=subject,start,end");
            if (response != null) 
            {
                Events = response.Events.ToList();

                //foreach (var item in response)
                //{
                //    Events.Add(new AgendaEvent()
                //    {
                //        Id = item.Id,
                //        Subject = item.Subject,
                //        StartDate = new AgendaEventDateTime()
                //        {
                //            DateTime = DateTime.Parse(item.Start.DateTime),
                //            TimeZone = item.Start.TimeZone

                //        },
                //        EndDate = new AgendaEventDateTime()
                //        {
                //            DateTime = DateTime.Parse(item.End.DateTime),
                //            TimeZone = item.End.TimeZone
                //        }
                //    });
                //}

                //Events = response.Value;
            }
        }
        catch (AccessTokenNotAvailableException exception)
        {
            exception.Redirect();
        }

        //var events = await CalendarEventsProvider.GetEventsInMonthAsync(SelectedDate.Year, SelectedDate.Month);

        //Events = new List<AgendaEvent>();
        //foreach (var item in events)
        //{
        //    Events.Add(new AgendaEvent()
        //    {
        //        Id = item.Id,
        //        Subject = item.Subject,
        //        StartDate = new AgendaEventDateTime()
        //        {
        //            DateTime = DateTime.Parse(item.Start.DateTime),
        //            TimeZone = item.Start.TimeZone

        //        },
        //        EndDate = new AgendaEventDateTime()
        //        {
        //            DateTime = DateTime.Parse(item.End.DateTime),
        //            TimeZone = item.End.TimeZone
        //        }
        //    });
            
        //}



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

            Days = AgendaHelper.BuildMonthCalendarDays(SelectedDate.Year, SelectedDate.Month);

            try
            {
                var client = ClientFactory.CreateClient(nameof(AuthorizationGraphMessageHandler));

                var lastDayInMonth = DateTime.DaysInMonth(SelectedDate.Year, SelectedDate.Month);
                Events = new();
                var response = await client.GetFromJsonAsync<Core.Models.AgendaEvents>($"{Config.GetSection("MicrosoftGraph")["Version"]}/me/events?$filter=start/datetime ge '{SelectedDate.Year}-{SelectedDate.Month}-01T00:00' and end/dateTime le '{SelectedDate.Year}-{SelectedDate.Month}-{lastDayInMonth}T00:00'&$select=subject,start,end");
                if (response != null)
                {
                    Events = response.Events.ToList();
                    //foreach (var item in response)
                    //{
                    //    Events.Add(new AgendaEvent()
                    //    {
                    //        Id = item.Id,
                    //        Subject = item.Subject,
                    //        StartDate = new AgendaEventDateTime()
                    //        {
                    //            DateTime = DateTime.Parse(item.Start.DateTime),
                    //            TimeZone = item.Start.TimeZone

                    //        },
                    //        EndDate = new AgendaEventDateTime()
                    //        {
                    //            DateTime = DateTime.Parse(item.End.DateTime),
                    //            TimeZone = item.End.TimeZone
                    //        }
                    //    });
                    //}

                    //Events = response.Value;
                }
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }

        }

        SelectedDate = date;
        base.StateHasChanged();
    }

    public async Task OnUpsertConector(ConnectorBriefDTO item)
    {
        //if (SelectedDate.Month != date.Month)
        //{
        //    Days = AgendaHelper.BuildMonthCalendarDays(date.Year, date.Month);
        //    Events = await CalendarEventsProvider.GetEventsInMonthAsync(date.Year, date.Month);
        //}

        //SelectedDate = date;
        //base.StateHasChanged();
    }

}
