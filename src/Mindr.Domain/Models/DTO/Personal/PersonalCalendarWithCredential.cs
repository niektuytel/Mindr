using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Mindr.Domain.Enums;

namespace Mindr.Domain.Models.DTO.Personal
{
    public class PersonalCalendarWithCredential
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("calendar_id")]
        public string CalendarId { get; set; }

        [JsonPropertyName("calendar_from")]
        public CalendarFrom CalendarFrom { get; set; }

    }
}