namespace Mindr.Shared.Models.GoogleCalendar
{
    // https://developers.google.com/calendar/api/v3/reference/calendarList/list#examples
    public class GoogleCalendarList
    {

        public string kind { get; set; }
        public string etag { get; set; }
        public string nextSyncToken { get; set; }
        public Item[] items { get; set; }


        public class Item
        {
            public string kind { get; set; }
            public string etag { get; set; }
            public string id { get; set; }
            public string summary { get; set; }
            public string timeZone { get; set; }
            public string summaryOverride { get; set; }
            public string colorId { get; set; }
            public string backgroundColor { get; set; }
            public string foregroundColor { get; set; }
            public bool selected { get; set; }
            public string accessRole { get; set; }
            public Defaultreminder[] defaultReminders { get; set; }
            public Conferenceproperties conferenceProperties { get; set; }
            public Notificationsettings notificationSettings { get; set; }
            public bool primary { get; set; }
            public string description { get; set; }
        }

        public class Conferenceproperties
        {
            public string[] allowedConferenceSolutionTypes { get; set; }
        }

        public class Notificationsettings
        {
            public Notification[] notifications { get; set; }
        }

        public class Notification
        {
            public string type { get; set; }
            public string method { get; set; }
        }

        public class Defaultreminder
        {
            public string method { get; set; }
            public int minutes { get; set; }
        }

    }
}
