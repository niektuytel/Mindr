using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Services
{
    public interface IApiPersonalCalendarClient
    {
        Task<JsonResponse<PersonalCalendar>> Insert(PersonalCalendarWithCredential calendar);
        Task<JsonResponse<PersonalCalendar>> Delete(string calendarId);
        Task<JsonResponse<IEnumerable<PersonalCalendar>>> GetAllCalendars();
        Task<JsonResponse<IEnumerable<CalendarAppointment>>> GetAllAppointments(DateTime dateStart, DateTime dateEnd, string? calendarId = null);
        Task<JsonResponse<CalendarAppointment>> InsertAppointment(string? calendarId, CalendarAppointment appointment);
        Task<JsonResponse<CalendarAppointment>> UpdateAppointment(string? calendarId, CalendarAppointment appointment);
        Task<JsonResponse<CalendarAppointment>> DeleteAppointment(string? calendarId, CalendarAppointment appointment);
    }
}