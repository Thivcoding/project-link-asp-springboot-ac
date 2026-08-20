using System.Configuration;

namespace LegacyAdmin_Asp.Config
{
    public static class SpringBootConfig
    {
        // =====================================================
        // SPRING BOOT BASE URL
        // =====================================================

        public static string BaseUrl
        {
            get
            {
                return ConfigurationManager
                    .AppSettings["SpringBootBaseUrl"];
            }
        }


        // =====================================================
        // AUTH
        // =====================================================

        public static string AuthLoginUrl
        {
            get
            {
                return BaseUrl + "/api/auth/login";
            }
        }


        public static string AuthRefreshUrl
        {
            get
            {
                return BaseUrl + "/api/auth/refresh";
            }
        }


        public static string AuthLogoutUrl
        {
            get
            {
                return BaseUrl + "/api/auth/logout";
            }
        }


        // =====================================================
        // SSO
        // =====================================================

        public static string SsoIssueUrl
        {
            get
            {
                return BaseUrl + "/api/sso/issue";
            }
        }


        public static string SsoExchangeUrl
        {
            get
            {
                return BaseUrl + "/api/sso/exchange";
            }
        }


        public static string SsoValidateUrl
        {
            get
            {
                return BaseUrl + "/api/sso/validate";
            }
        }


        public static string SsoRevokeUrl
        {
            get
            {
                return BaseUrl + "/api/sso/revoke";
            }
        }
    }
}