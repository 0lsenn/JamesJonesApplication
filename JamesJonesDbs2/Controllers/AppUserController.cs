using JamesJonesApplication.Services;
using JamesJonesDbs2.Models;
using JamesJonesDbs2.Models.Database;
using JamesJonesDbs2.Models.DataTransferObject;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Claims;

namespace JamesJonesDbs2.Controllers
{
    /// <summary>
    /// Controller dedicated for managing App users of the application
    /// </summary>
    public class AppUserController : Controller
    {
        //Database & Auth dependency injection
        private readonly DatabaseContext _appUserContext;

        private readonly AuthRepository _authRepository;

        //Constructor
        public AppUserController(DatabaseContext appUserContext, AuthRepository authRepository)
        {
            _appUserContext = appUserContext;
            _authRepository = authRepository;
        }


        /// <summary>
        /// Login view
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Login()
        {
            Thread.Sleep(1000);
            return View();
        }

        /// <summary>
        /// Validate and confirm the user from the database and assign session items
        /// </summary>
        /// <param name="loginDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginConfirm(LoginAppUserDTO loginDetails)
        {
            
            //checks credentials and authenticate them
            var userToAccess = _authRepository.Authenticate(loginDetails);

            try
            {
                //null check
                if (userToAccess == null)
                {
                    return View();
                }
                
                //Claims check
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Sid, userToAccess.Id.ToString()),
                    new Claim(ClaimTypes.Email, userToAccess.EmailAddress),
                    new Claim(ClaimTypes.Role, userToAccess.Role),
                    new Claim("ID", userToAccess.Id.ToString())
                };


                HttpContext.Session.SetString("EmailAddress", userToAccess.EmailAddress);

                //Cookie Authenticate
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //Property config
                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };

                //Collection of ClaimsIdentity objects that is accessible through the Identities property.
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                Thread.CurrentPrincipal = claimsPrincipal;

                //Sign in the user
                await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);


                return RedirectToAction("Index", "SamsWareHouseItem");
            }
            catch
            {
                return View("Login", "AppUser");
            }
        }

        /// <summary>
        /// Return view for registration
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Allows the user to create an instance of themselves as an account into the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(RegisterAppUserDTO registeredDetails)
        {
            try
            {
                Thread.Sleep(3000);

                AppUser newUser = new AppUser()
                {
                    EmailAddress = registeredDetails.EmailAddress,
                    PasswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(registeredDetails.Password),
                    Role = "Customer"
                };
                _appUserContext.AppUsers.Add(newUser);
                _appUserContext.SaveChanges();

                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                return RedirectToAction("Login","AppUser");
            }
        }

        /// <summary>
        /// Clears User set session items
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            Thread.Sleep(5000);

            await HttpContext.SignOutAsync();
            HttpContext.Session.Clear();

            return RedirectToAction("Index","Home");
        }

    }
}
