using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;
using Mindr.WebAssembly.Client.Models.Options;
using Mindr.WebAssembly.Client.Handlers;
using System.Text;
using System.Text.Json.Serialization;
using Mindr.Domain;
using System.Text.Json;

using Mindr.Domain.HttpRunner.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.Fast.Components.FluentUI;
using Microsoft.Extensions.Logging;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.WebAssembly.Client.Models;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.Domain.Models.DTO.Calendar;

namespace Mindr.WebAssembly.Client.Services;

public class ApiPersonalCalendarClient : ApiClientBase, IApiPersonalCalendarClient
{
    private static readonly string Path = "api/personalcalendar";
    private static readonly string HttpClientName = nameof(AuthorizedMindrApiHandler);

    public ApiPersonalCalendarClient(IJSRuntime JSRuntime, IHttpClientFactory factory)
        : base(JSRuntime, factory.CreateClient(HttpClientName))
    {
    }

    public async Task<JsonResponse<IEnumerable<PersonalCalendar>>> GetCalendars()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Path}");
        var response = await ApiRequest<IEnumerable<PersonalCalendar>>(request);
        return response;
    }

    public async Task<JsonResponse<IEnumerable<PersonalCalendar>>> GetExternalCalendars()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Path}/external");
        var response = await ApiRequest<IEnumerable<PersonalCalendar>>(request);
        return response;
    }

    public async Task<JsonResponse<PersonalCalendar>> InsertCalendar(PersonalCalendar calendar)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{Path}");
        request.Headers.Add("accept", "*/*");

        var content = JsonSerializer.Serialize(calendar);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await ApiRequest<PersonalCalendar>(request);
        return response;
    }

    public async Task<JsonResponse<PersonalCalendar>> DeleteCalendar(string calendarId)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{Path}/{calendarId}");
        var response = await ApiRequest<PersonalCalendar>(request);
        return response;
    }

    public async Task<JsonResponse<IEnumerable<CalendarAppointment>>> GetAppointments(DateTime dateStart, DateTime dateEnd, string calendarId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{Path}/appointments?calendarId={calendarId}&dateTimeStart={dateStart}&dateTimeEnd={dateEnd}");
        var response = await ApiRequest<IEnumerable<CalendarAppointment>>(request);
        return response;
    }

    public async Task<JsonResponse<CalendarAppointment>> InsertAppointment(string calendarId, CalendarAppointment appointment)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"{Path}/{calendarId}/appointment");
        request.Headers.Add("accept", "*/*");

        var content = JsonSerializer.Serialize(appointment);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await ApiRequest<CalendarAppointment>(request);
        return response;
    }

    public async Task<JsonResponse<CalendarAppointment>> UpdateAppointment(string calendarId, CalendarAppointment appointment)
    {
        var request = new HttpRequestMessage(HttpMethod.Put, $"{Path}/{calendarId}/appointment/{appointment.Id}");
        request.Headers.Add("accept", "*/*");

        var content = JsonSerializer.Serialize(appointment);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await ApiRequest<CalendarAppointment>(request);
        return response;
    }

    public async Task<JsonResponse<CalendarAppointment>> DeleteAppointment(string calendarId, CalendarAppointment appointment)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{Path}/{calendarId}/appointment/{appointment.Id}");
        request.Headers.Add("accept", "*/*");

        var content = JsonSerializer.Serialize(appointment);
        request.Content = new StringContent(content, Encoding.UTF8, "application/json");

        var response = await ApiRequest<CalendarAppointment>(request);
        return response;
    }
}
