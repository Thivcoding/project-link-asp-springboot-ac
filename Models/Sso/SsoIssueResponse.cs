using Newtonsoft.Json;

namespace LegacyAdmin_Asp.Models.Sso
{
    public class SsoIssueResponse
    {
        [JsonProperty("ticket")]
        public string Ticket { get; set; }

        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }
    }
}