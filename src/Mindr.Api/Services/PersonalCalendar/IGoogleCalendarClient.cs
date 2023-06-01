using Google.Apis.Calendar.v3.Data;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;

namespace Mindr.Api.Services.CalendarEvents
{
    public interface IGoogleCalendarClient
    {
        Task<string> GetAccessToken(PersonalCredential credential);
        Task<IEnumerable<PersonalCalendar>> GetCalendars(PersonalCredential credential, string userId);
        Task<IEnumerable<CalendarEvent>?> GetCalendarEvents(PersonalCredential credential, DateTime startDateTime, DateTime endDateTime, string calendarId);
    }
}