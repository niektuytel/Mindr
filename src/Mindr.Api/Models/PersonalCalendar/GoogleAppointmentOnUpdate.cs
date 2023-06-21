using Mindr.Domain.Models.DTO.Calendar;
using System.Drawing;
using System.Text.Json.Serialization;

namespace Mindr.Api.Models.PersonalCalendar
{
    public class GoogleAppointmentOnUpdate
    {
        public GoogleAppointmentOnUpdate(CalendarAppointment appointment)
        {
            StartDate = appointment.StartDate;
            EndDate = appointment.EndDate;
            Subject = appointment.Subject;
        }

        [JsonPropertyName("subject")]
        public string Subject { get; set; } = "";

        [JsonPropertyName("start")]
        public CalendarEventDateTime StartDate { get; set; } = new();

        [JsonPropertyName("end")]
        public CalendarEventDateTime EndDate { get; set; } = new();
    }
}
