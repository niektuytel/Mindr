using System;

namespace Mindr.Core.Models
{
    public class CalendarEvent
    {
        public CalendarEvent()
        {
            Color = GetRandomColorClass();
        }

        public string Subject { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Color { get; set; } = GetRandomColorClass();

        private static string GetRandomColorClass(){
            string[] colors = new[] {"magenta", "yellow", "green", "pink", "red"};
            var random = new Random();
            return colors[random.Next(0, colors.Length)];
        }
    }
}