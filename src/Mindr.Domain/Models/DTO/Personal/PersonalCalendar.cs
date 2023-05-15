using Mindr.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mindr.Domain.Models.DTO.Personal
{
    public class PersonalCalendar
    {
        public PersonalCalendar()
        {
            
        }

        public PersonalCalendar(string userId, string calendarId, Guid credentialId, CalendarFrom from)
        {
            UserId = userId;
            CalendarId = calendarId;
            CredentialId = credentialId;
            From = from;
        }

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserId { get; set; }

        public Guid CredentialId { get; set; }

        public CalendarFrom From { get; set; }

        public string CalendarId { get; set; }

        public string Description { get; set; }

        public string Summary { get; set; }

        public bool Selected { get; set; }

        public string Color { get; set; }

    }
}
