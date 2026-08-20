using System;
using System.Web;

namespace LegacyAdmin_Asp.Helpers
{
    public static class CookieHelper
    {
        public const string SessionCookie =
            "AUTH_SESSION";

        public const string RefreshCookie =
            "AUTH_REFRESH";


        // =====================================================
        // SAVE AUTH COOKIES
        // =====================================================

        public static void SaveAuthCookies(
            string sessionId,
            string refreshToken)
        {
            HttpContext context =
                HttpContext.Current;


            if (context == null)
            {
                return;
            }


            // =================================================
            // SESSION COOKIE
            // =================================================

            HttpCookie sessionCookie =
                new HttpCookie(
                    SessionCookie,
                    sessionId
                );

            sessionCookie.HttpOnly = true;

            sessionCookie.Secure =
                context.Request.IsSecureConnection;

            sessionCookie.Expires =
                DateTime.UtcNow.AddDays(7);


            context.Response.Cookies.Add(
                sessionCookie
            );


            // =================================================
            // REFRESH COOKIE
            // =================================================

            HttpCookie refreshCookie =
                new HttpCookie(
                    RefreshCookie,
                    refreshToken
                );

            refreshCookie.HttpOnly = true;

            refreshCookie.Secure =
                context.Request.IsSecureConnection;

            refreshCookie.Expires =
                DateTime.UtcNow.AddDays(7);


            context.Response.Cookies.Add(
                refreshCookie
            );
        }


        // =====================================================
        // GET SESSION ID
        // =====================================================

        public static string GetSessionId()
        {
            HttpContext context =
                HttpContext.Current;


            if (context == null)
            {
                return null;
            }


            HttpCookie cookie =
                context.Request.Cookies[
                    SessionCookie
                ];


            return cookie?.Value;
        }


        // =====================================================
        // GET REFRESH TOKEN
        // =====================================================

        public static string GetRefreshToken()
        {
            HttpContext context =
                HttpContext.Current;


            if (context == null)
            {
                return null;
            }


            HttpCookie cookie =
                context.Request.Cookies[
                    RefreshCookie
                ];


            return cookie?.Value;
        }


        // =====================================================
        // DELETE AUTH COOKIES
        // =====================================================

        public static void DeleteAuthCookies()
        {
            ExpireCookie(SessionCookie);
            ExpireCookie(RefreshCookie);
        }


        // =====================================================
        // EXPIRE COOKIE
        // =====================================================

        private static void ExpireCookie(
            string name)
        {
            HttpContext context =
                HttpContext.Current;


            if (context == null)
            {
                return;
            }


            HttpCookie cookie =
                new HttpCookie(name);


            cookie.Value = "";

            cookie.Expires =
                DateTime.UtcNow.AddDays(-1);

            cookie.HttpOnly = true;

            cookie.Secure =
                context.Request.IsSecureConnection;


            context.Response.Cookies.Add(
                cookie
            );
        }
    }
}