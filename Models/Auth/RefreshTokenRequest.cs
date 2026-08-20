using Newtonsoft.Json;

namespace LegacyAdmin_Asp.Models.Auth
{
    public class RefreshTokenRequest
    {
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }
}