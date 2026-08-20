using LegacyAdmin_Asp.Filters;
using LegacyAdmin_Asp.Helpers;
using LegacyAdmin_Asp.Models.Sso;
using LegacyAdmin_Asp.Services;
using LegacyAdmin_Asp.Services.Interface;

using System;
using System.Web.Mvc;

namespace LegacyAdmin_Asp.Controllers
{
    public class SsoController : Controller
    {
        private readonly ISsoService _ssoService;

        public SsoController()
        {
            _ssoService =
                new SsoService();
        }


        // =====================================================
        // OPEN NEXT.JS
        // =====================================================

        [HttpGet]
        [RedisAuthorize]
        public ActionResult OpenNextApp()
        {
            try
            {
                // =============================================
                // 1. Get ASP.NET session
                // =============================================

                string sessionId =
                    CookieHelper.GetSessionId();


                if (
                    string.IsNullOrWhiteSpace(
                        sessionId
                    )
                )
                {
                    return RedirectToAction(
                        "Login",
                        "Account"
                    );
                }


                // =============================================
                // 2. Ask Spring Boot for ticket
                // =============================================

                SsoIssueResponse response =
                    _ssoService.IssueTicket(
                        sessionId
                    );


                if (
                    response == null ||
                    string.IsNullOrWhiteSpace(
                        response.Ticket
                    )
                )
                {
                    throw new Exception(
                        "SSO ticket was not generated."
                    );
                }


                // =============================================
                // 3. Redirect to Next.js
                // =============================================

                string nextUrl =
                    System.Configuration
                        .ConfigurationManager
                        .AppSettings[
                            "NextJsBaseUrl"
                        ];


                string url =
                    nextUrl
                    + "/sso/callback?ticket="
                    + Uri.EscapeDataString(
                        response.Ticket
                    );


                return Redirect(url);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(
                    500,
                    "SSO Error: " + ex.Message
                );
            }
        }
    }
}