using Mindr.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Mindr.Domain.Models.DTO.Personal
{
    public class PersonalCredential
    {
        public PersonalCredential()
        {
            
        }

        public PersonalCredential(Guid id, string userId, PersonalCredentialDTO dto)
        {
            Id = id;
            UserId = userId;
            Target = dto.Target;
            AccessToken = dto.AccessToken;
            RefreshToken = dto.RefreshToken;
            Scope = dto.Scope;
            TokenType = dto.TokenType;
            ExpiresIn = dto.ExpiresIn;
        }

        public PersonalCredential(string userId, PersonalCredentialDTO dto)
        {
            UserId = userId;
            Target = dto.Target;
            AccessToken = dto.AccessToken;
            RefreshToken = dto.RefreshToken;
            Scope = dto.Scope;
            TokenType = dto.TokenType;
            ExpiresIn = dto.ExpiresIn;
        }

        public PersonalCredential(string userId, CredentialTarget target, string accessToken, string refreshToken, string scope, string tokenType, int expiresIn)
        {
            UserId = userId;
            Target = target;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Scope = scope;
            TokenType = tokenType;
            ExpiresIn = expiresIn;
        }

        public PersonalCredential(CredentialTarget target, string accessToken, string refreshToken, string scope, string tokenType, int expiresIn)
        {
            Target = target;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            Scope = scope;
            TokenType = tokenType;
            ExpiresIn = expiresIn;
        }

        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonIgnore]
        public CredentialTarget Target { get; set; }

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
