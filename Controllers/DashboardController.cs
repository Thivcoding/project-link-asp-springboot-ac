using LegacyAdmin_Asp.Filters;
using LegacyAdmin_Asp.Helpers;

using System.Web.Mvc;

namespace LegacyAdmin_Asp.Controllers
{
    [RedisAuthorize]
    public class DashboardController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var currentUser =
                CurrentUserHelper.Get();

            if (currentUser == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account"
                );
            }

            ViewBag.UserId =
                currentUser.Id;

            ViewBag.Username =
                currentUser.Username;

            ViewBag.Email =
                currentUser.Email;

            ViewBag.RoleName =
                currentUser.RoleName;

            ViewBag.UserProfile =
                currentUser.FirstName
                + " "
                + currentUser.LastName;

            ViewBag.Active =
                currentUser.Active;

            return View();
        }
    }
}