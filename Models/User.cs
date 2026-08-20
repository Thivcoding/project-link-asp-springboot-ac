namespace LegacyAdmin_Asp.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string UserProfile { get; set; }

        public bool Status { get; set; }
    }
}