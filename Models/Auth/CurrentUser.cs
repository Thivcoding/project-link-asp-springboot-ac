namespace LegacyAdmin_Asp.Models.Auth
{
    public class CurrentUser
    {
        public long Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string RoleName { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAuthenticated { get; set; }
    }
}