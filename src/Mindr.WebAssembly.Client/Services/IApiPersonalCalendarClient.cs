using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;
using Mindr.WebAssembly.Client.Models;

namespace Mindr.WebAssembly.Client.Services
{
    public interface IApiPersonalCalendarClient
    {
        Task<JsonResponse<PersonalCalendar>> Create(PersonalCalendarWithCredential calendar);
        Task<JsonResponse<PersonalCalendar>> Delete(string calendarId);
        Task<JsonResponse<IEnumerable<PersonalCalendar>>> GetAllCalendars();
        Task<JsonResponse<IEnumerable<CalendarEvent>>> GetAllEvents(DateTime dateStart, DateTime dateEnd, string? calendarId);
    }
}