using Mindr.Domain.HttpRunner.Models;
using Mindr.Domain.Models.DTO.Calendar;
using Mindr.Domain.Models.DTO.Personal;

namespace Mindr.Api.Services.CalendarEvents
{
    public interface IPersonalCalendarManager
    {
        Task<IEnumerable<PersonalCalendar>> GetCalendars(string userId);
        Task<IEnumerable<PersonalCalendar>> GetExternalCalendars(string userId);
        Task<PersonalCalendar> InsertCalendar(string userId, PersonalCalendar input);
        Task<PersonalCalendar> DeleteCalendar(string userId, string calendarId);

        Task<IEnumerable<CalendarAppointment>> GetAppointments(string userId, DateTime dateTimeStart, DateTime dateTimeEnd, string? calendarId = null);
        Task<CalendarAppointment> InsertAppointment(string userId, string calendarId, CalendarAppointment input);
        Task<CalendarAppointment> UpdateAppointment(string userId, string calendarId, string appointmentId, CalendarAppointment input);
        Task<CalendarAppointment> DeleteAppointment(string userId, string calendarId, string appointmentId);
    }
}