
using Mindr.Api.Models.PersonalCalendar;
using Mindr.Domain.Models.DTO.Calendar;

namespace Mindr.Api.Extensions
{
    public static class CalendarAppointmentExtensions
    {

        public static CalendarEventDateTime AsCalendarEventDateTime(this Google.Apis.Calendar.v3.Data.EventDateTime eventDateTime)
        {
            return new CalendarEventDateTime
            {
                Date = eventDateTime.Date,
                DateTime = eventDateTime.DateTime,
                TimeZone = eventDateTime.TimeZone
            };
        }
    }
}
