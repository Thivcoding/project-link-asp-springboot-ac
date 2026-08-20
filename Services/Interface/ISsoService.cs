using LegacyAdmin_Asp.Models.Sso;

namespace LegacyAdmin_Asp.Services.Interface
{
    public interface ISsoService
    {
        SsoIssueResponse IssueTicket(
            string sessionId
        );
    }
}