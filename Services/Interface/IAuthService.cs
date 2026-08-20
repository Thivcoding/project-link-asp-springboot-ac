using LegacyAdmin_Asp.Models.Auth;

namespace LegacyAdmin_Asp.Services.Interface
{
    public interface IAuthService
    {
        LoginResponse Login(
            string username,
            string password
        );

        bool ValidateSession(
            string sessionId
        );

        void Logout(
            string refreshToken
        );

        LoginResponse RefreshToken(
            string refreshToken
        );
    }
}