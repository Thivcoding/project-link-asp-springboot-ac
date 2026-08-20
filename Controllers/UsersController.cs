using LegacyAdmin_Asp.Filters;
using LegacyAdmin_Asp.Helpers;

using System.Web.Mvc;

namespace LegacyAdmin_Asp.Controllers
{
    [RedisAuthorize]
    public class UsersController : Controller
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

            ViewBag.UserProfile =
                currentUser.FirstName
                + " "
                + currentUser.LastName;

            ViewBag.RoleName =
                currentUser.RoleName;

            ViewBag.Active =
                currentUser.Active;

            return View();
        }
    }
}