using System;

namespace Mindr.Domain.Models.GoogleCalendar
{
    // https://developers.google.com/calendar/api/v3/reference/events/list?apix_params=%7B%22calendarId%22%3A%22angeliquehak5%40gmail.com%22%7D#try-it
    public class GoogleCalendarEvents
    {
        public string kind { get; set; }
        public string etag { get; set; }
        public string summary { get; set; }
        public DateTime updated { get; set; }
        public string timeZone { get; set; }
        public string accessRole { get; set; }
        public object[] defaultReminders { get; set; }
        public string nextSyncToken { get; set; }
        public Item[] items { get; set; }

        public class Item
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public string status { get; set; }
            public string htmlLink { get; set; }
            public DateTime created { get; set; }
            public DateTime updated { get; set; }
            public string summary { get; set; }
            public string description { get; set; }
            public string location { get; set; }
            public Creator creator { get; set; }
            public Organizer organizer { get; set; }
            public Start start { get; set; }
            public End end { get; set; }
            public string iCalUID { get; set; }
            public int sequence { get; set; }
            public Reminders reminders { get; set; }
            public string eventType { get; set; }
            public string[] recurrence { get; set; }
            public string transparency { get; set; }
            public string recurringEventId { get; set; }
            public Originalstarttime originalStartTime { get; set; }
            public Attendee[] attendees { get; set; }
            public bool guestsCanInviteOthers { get; set; }
            public bool privateCopy { get; set; }
        }

        public class Creator
        {
            public string email { get; set; }
            public bool self { get; set; }
        }

        public class Organizer
        {
            public string email { get; set; }
            public bool self { get; set; }
            public string displayName { get; set; }
        }

        public class Start
        {
            public DateTime dateTime { get; set; }
            public string timeZone { get; set; }
            public string date { get; set; }
        }

        public class End
        {
            public DateTime dateTime { get; set; }
            public string timeZone { get; set; }
            public string date { get; set; }
        }

        public class Reminders
        {
            public bool useDefault { get; set; }
        }

        public class Originalstarttime
        {
            public string date { get; set; }
            public DateTime dateTime { get; set; }
            public string timeZone { get; set; }
        }

        public class Attendee
        {
            public string email { get; set; }
            public bool self { get; set; }
            public string responseStatus { get; set; }
            public string displayName { get; set; }
            public bool optional { get; set; }
        }

    }
}
