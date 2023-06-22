using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Connector;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Services
{
    public interface IApiPersonalCalendarClient
    {
        Task<JsonResponse<IEnumerable<PersonalCalendar>>> GetCalendars();
        Task<JsonResponse<IEnumerable<PersonalCalendar>>> GetExternalCalendars();
        Task<JsonResponse<PersonalCalendar>> InsertCalendar(PersonalCalendar calendar);
        Task<JsonResponse<PersonalCalendar>> DeleteCalendar(string calendarId);

        Task<JsonResponse<IEnumerable<CalendarAppointment>>> GetAppointments(DateTime dateStart, DateTime dateEnd, string? calendarId = null);
        Task<JsonResponse<CalendarAppointment>> InsertAppointment(string? calendarId, CalendarAppointment appointment);
        Task<JsonResponse<CalendarAppointment>> UpdateAppointment(string? calendarId, CalendarAppointment appointment);
        Task<JsonResponse<CalendarAppointment>> DeleteAppointment(string? calendarId, CalendarAppointment appointment);

        Task<JsonResponse<IEnumerable<ConnectorEvent>>> GetConnectorEvents(string? calendarId);
        Task<JsonResponse<ConnectorEvent>> InsertConnectorEvent(string calendarId, ConnectorEvent connectorEvent);
        Task<JsonResponse<ConnectorEvent>> UpdateConnectorEvent(ConnectorEvent connectorEvent);
        Task<JsonResponse<ConnectorEvent>> DeleteConnectorEvent(Guid connectorEventId);
    }
}