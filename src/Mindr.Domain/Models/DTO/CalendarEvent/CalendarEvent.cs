using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Mindr.Domain.Models.DTO.CalendarEvent
{
    public class CalendarEvent
    {
        public CalendarEvent()
        {
            
        }

        public CalendarEvent(string userId, CalendarEventDTO calendarEventDTO)
        {
            UserId = userId;

        }

        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }


    }
}
