using Google.Apis.Calendar.v3.Data;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;

namespace Mindr.Api.Services.CalendarEvents
{
    public interface ICalendarClient
    {
        Task<string> GetAccessToken(PersonalCredential credential);
        Task<IEnumerable<PersonalCalendar>> GetCalendars(PersonalCredential credential, string userId);
        Task<IEnumerable<CalendarAppointment>> GetCalendarAppointments(PersonalCredential personalCredential, string calendarId, DateTime startDateTime, DateTime endDateTime);
        Task<CalendarAppointment> InsertCalendarAppointment(PersonalCredential personalCredential, string calendarId, CalendarAppointment input);
        Task<CalendarAppointment> UpdateCalendarAppointment(PersonalCredential personalCredential, string calendarId, string appointmentId, CalendarAppointment input);
        Task<CalendarAppointment> DeleteCalendarAppointment(PersonalCredential personalCredential, string calendarId, string appointmentId);
    }
}