using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace LegacyAdmin_Asp.Models.Auth
{
    public class LoginRequest
    {
        [JsonProperty("username")]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}