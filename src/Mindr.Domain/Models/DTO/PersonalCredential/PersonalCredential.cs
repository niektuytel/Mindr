using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mindr.Domain.Models.DTO.PersonalCredential
{
    public class PersonalCredential
    {
        public PersonalCredential()
        {
            
        }
        public PersonalCredential(string userId, PersonalCredentialDTO dto)
        {
            UserId = userId;
            AccessToken = dto.AccessToken;
            RefreshToken = dto.RefreshToken;
            Scope = dto.Scope;
            TokenType = dto.TokenType;
            ExpiresIn = dto.ExpiresIn;
        }

        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
    }
}
