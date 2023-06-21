using System.Text.Json.Serialization;
using System;

namespace Mindr.Domain.Models.DTO.Calendar
{
    public class CalendarEventDateTime
    {
        //
        // Summary:
        //     The date, in the format "yyyy-mm-dd", if this is an all-day event.
        [JsonPropertyName("date")]
        public virtual string Date { get; set; }

        //
        // Summary:
        //     The time, as a combined date-time value (formatted according to RFC3339). A time
        //     zone offset is required unless a time zone is explicitly specified in timeZone.
        [JsonPropertyName("dateTime")]
        public virtual string DateTimeRaw { get; set; }

        //
        // Summary:
        //     System.DateTime representation of Google.Apis.Calendar.v3.Data.EventDateTime.DateTimeRaw.
        [JsonIgnore]
        public virtual DateTime? DateTime
        {
            get
            {
                if(System.DateTime.TryParse(DateTimeRaw, out var result))
                {
                    return result;
                }

                return null;
            }
            set
            {
                DateTimeRaw = value.ToString();
            }
        }

        //
        // Summary:
        //     The time zone in which the time is specified. (Formatted as an IANA Time Zone
        //     Database name, e.g. "Europe/Zurich".) For recurring events this field is required
        //     and specifies the time zone in which the recurrence is expanded. For single events
        //     this field is optional and indicates a custom time zone for the event start/end.
        [JsonPropertyName("timeZone")]
        public virtual string TimeZone { get; set; }

        //
        // Summary:
        //     The ETag of the item.
        public virtual string ETag { get; set; }
    }
}