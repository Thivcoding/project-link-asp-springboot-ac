using LegacyAdmin_Asp.Filters;
using System.Web.Mvc;

namespace LegacyAdmin_Asp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(
                new RedisAuthorizeAttribute()
            );
        }
    }
}
