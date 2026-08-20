using LegacyAdmin_Asp.Helpers;
using LegacyAdmin_Asp.Services;
using LegacyAdmin_Asp.Services.Interface;

using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LegacyAdmin_Asp.Filters
{
    public class RedisAuthorizeAttribute
        : AuthorizeAttribute
    {
        private readonly IAuthService _authService;


        public RedisAuthorizeAttribute()
        {
            _authService =
                new AuthService();
        }


        protected override bool AuthorizeCore(
            HttpContextBase httpContext)
        {
            string sessionId =
                CookieHelper.GetSessionId();


            // =============================================
            // NO SESSION COOKIE
            // =============================================

            if (string.IsNullOrWhiteSpace(sessionId))
            {
                return false;
            }


            try
            {
                // =============================================
                // VALIDATE SESSION IN SPRING BOOT / REDIS
                // =============================================

                bool valid =
                    _authService.ValidateSession(
                        sessionId
                    );


                if (!valid)
                {
                    CurrentUserHelper.Clear();

                    return false;
                }


                // =============================================
                // SESSION VALID
                // =============================================

                return true;
            }
            catch
            {
                CurrentUserHelper.Clear();

                return false;
            }
        }


        protected override void HandleUnauthorizedRequest(
            AuthorizationContext filterContext)
        {
            CurrentUserHelper.Clear();

            CookieHelper.DeleteAuthCookies();


            if (
                filterContext
                    .HttpContext
                    .Request
                    .IsAjaxRequest()
            )
            {
                filterContext.Result =
                    new HttpStatusCodeResult(
                        401
                    );

                return;
            }


            filterContext.Result =
                new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Account",
                            action = "Login"
                        }
                    )
                );
        }
    }
}