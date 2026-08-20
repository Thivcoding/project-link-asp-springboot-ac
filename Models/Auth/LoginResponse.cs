using Newtonsoft.Json;

namespace LegacyAdmin_Asp.Models.Auth
{
    public class LoginResponse
    {
        [JsonProperty("sessionId")]
        public string SessionId { get; set; }

        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("tokenType")]
        public string TokenType { get; set; }

        [JsonProperty("expiresIn")]
        public long ExpiresIn { get; set; }

        [JsonProperty("user")]
        public AuthUser User { get; set; }
    }
}