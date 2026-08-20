using LegacyAdmin_Asp.Models.Auth;
using System.Web;

namespace LegacyAdmin_Asp.Helpers
{
    public static class CurrentUserHelper
    {
        private const string CurrentUserKey =
            "CURRENT_USER";


        // =====================================================
        // SET
        // =====================================================

        public static void Set(
            CurrentUser user)
        {
            if (HttpContext.Current == null)
            {
                return;
            }

            if (HttpContext.Current.Session == null)
            {
                return;
            }

            HttpContext.Current.Session[
                CurrentUserKey
            ] = user;
        }


        // =====================================================
        // GET
        // =====================================================

        public static CurrentUser Get()
        {
            if (HttpContext.Current == null)
            {
                return null;
            }

            if (HttpContext.Current.Session == null)
            {
                return null;
            }

            return HttpContext.Current.Session[
                CurrentUserKey
            ] as CurrentUser;
        }


        // =====================================================
        // IS AUTHENTICATED
        // =====================================================

        public static bool IsAuthenticated()
        {
            var user = Get();

            return user != null
                && user.IsAuthenticated
                && user.Active;
        }


        // =====================================================
        // CLEAR
        // =====================================================

        public static void Clear()
        {
            if (HttpContext.Current == null)
            {
                return;
            }

            if (HttpContext.Current.Session == null)
            {
                return;
            }

            HttpContext.Current.Session.Remove(
                CurrentUserKey
            );
        }
    }
}