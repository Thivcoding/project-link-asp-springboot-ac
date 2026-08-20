using LegacyAdmin_Asp.Helpers;
using LegacyAdmin_Asp.Models.Auth;
using LegacyAdmin_Asp.Services;
using LegacyAdmin_Asp.Services.Interface;

using System;
using System.Web.Mvc;

namespace LegacyAdmin_Asp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;


        public AccountController()
        {
            _authService =
                new AuthService();
        }


        // =====================================================
        // LOGIN PAGE
        // =====================================================

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View(
                new LoginRequest()
            );
        }


        // =====================================================
        // LOGIN
        // =====================================================

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(
            LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }


            try
            {
                LoginResponse response =
                    _authService.Login(
                        request.Username,
                        request.Password
                    );


                if (response == null)
                {
                    ModelState.AddModelError(
                        "",
                        "Invalid login response."
                    );

                    return View(request);
                }


                if (
                    string.IsNullOrWhiteSpace(
                        response.SessionId
                    )
                )
                {
                    ModelState.AddModelError(
                        "",
                        "Session ID was not returned."
                    );

                    return View(request);
                }


                if (
                    string.IsNullOrWhiteSpace(
                        response.RefreshToken
                    )
                )
                {
                    ModelState.AddModelError(
                        "",
                        "Refresh token was not returned."
                    );

                    return View(request);
                }


                if (response.User == null)
                {
                    ModelState.AddModelError(
                        "",
                        "User information was not returned."
                    );

                    return View(request);
                }


                // =============================================
                // SAVE COOKIES
                // =============================================

                CookieHelper.SaveAuthCookies(
                    response.SessionId,
                    response.RefreshToken
                );


                // =============================================
                // CREATE CURRENT USER
                // =============================================

                CurrentUser currentUser =
                    new CurrentUser
                    {
                        Id =
                            response.User.Id,

                        Username =
                            response.User.Username,

                        Email =
                            response.User.Email,

                        RoleName =
                            response.User.RoleName,

                        Active =
                            response.User.Active,

                        FirstName =
                            response.User.FirstName,

                        LastName =
                            response.User.LastName,

                        IsAuthenticated =
                            true
                    };


                // =============================================
                // SAVE ASP.NET SESSION
                // =============================================

                CurrentUserHelper.Set(
                    currentUser
                );


                // =============================================
                // DASHBOARD
                // =============================================

                return RedirectToAction(
                    "Index",
                    "Dashboard"
                );
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    ex.Message
                );

                return View(request);
            }
        }


        // =====================================================
        // LOGOUT
        // =====================================================

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            try
            {
                // =============================================
                // 1. GET REFRESH TOKEN
                // =============================================

                string refreshToken =
                    CookieHelper.GetRefreshToken();


                // =============================================
                // 2. CALL SPRING BOOT LOGOUT
                // =============================================

                if (
                    !string.IsNullOrWhiteSpace(
                        refreshToken
                    )
                )
                {
                    _authService.Logout(
                        refreshToken
                    );
                }
            }
            catch
            {
                // =============================================
                // IMPORTANT
                //
                // Even if Spring Boot is unavailable,
                // ASP.NET must still logout locally.
                // =============================================
            }
            finally
            {
                // =============================================
                // 3. CLEAR CURRENT USER
                // =============================================

                CurrentUserHelper.Clear();


                // =============================================
                // 4. DELETE AUTH COOKIES
                // =============================================

                CookieHelper.DeleteAuthCookies();
            }


            // =============================================
            // 5. REDIRECT LOGIN
            // =============================================

            return RedirectToAction(
                "Login",
                "Account"
            );
        }
    }
}