
using Microsoft.Graph;
using Mindr.Domain.Models.DTO.Calendar;

namespace Mindr.Api.Extensions
{
    public static class CalendarAppointmentExtensions
    {

        public static CalendarEventDateTime AsCalendarEventDateTime(this Google.Apis.Calendar.v3.Data.EventDateTime eventDateTime)
        {
            var isParsed = DateTime.TryParse(eventDateTime.Date, out var result);
            if (isParsed)
            {
                return new CalendarEventDateTime
                {
                    Date = result,
                    DateTime = eventDateTime.DateTime,
                    TimeZone = eventDateTime.TimeZone
                };
            }

            return new CalendarEventDateTime
            {
                DateTime = eventDateTime.DateTime,
                TimeZone = eventDateTime.TimeZone
            };
        }

        public static Google.Apis.Calendar.v3.Data.EventDateTime AsGoogleEventDateTime(this CalendarEventDateTime eventDateTime)
        {
            var googleEventDateTime = new Google.Apis.Calendar.v3.Data.EventDateTime
            {
                DateTime = eventDateTime.DateTime,
                TimeZone = eventDateTime.TimeZone
            };

            if(eventDateTime.Date != null)
            {
                googleEventDateTime.Date = eventDateTime.Date.Value.ToString("yyyy-MM-dd");
            }

            return googleEventDateTime;
        }

        public static Google.Apis.Calendar.v3.Data.Event AsGoogleEvent(this CalendarAppointment appointment)
        {
            var googleEvent = new Google.Apis.Calendar.v3.Data.Event()
            {
                Summary = appointment.Subject,
                Start = appointment.StartDate.AsGoogleEventDateTime(),
                End = appointment.EndDate.AsGoogleEventDateTime()
            };

            return googleEvent;
        }


    }
}
