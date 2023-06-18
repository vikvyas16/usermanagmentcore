using DemoApplication.BusinessEntity;
using DemoApplication.Models;
using DemoApplication.Repository.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DemoApplication.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region Login Process

        // GET:Login 
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        //Post:Login 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel LoginViewModel)
        {
            if (IsAuthenitcatedUser(LoginViewModel.Email, LoginViewModel.Password))
            {
                //Login Authentication
                var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, LoginViewModel.Email), new Claim(ClaimTypes.Role, "User") }, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });
                HttpContext.Session.SetString("username", LoginViewModel.Email);
                return RedirectToAction("Index", "User");
            }
            else
            {
                ModelState.AddModelError("", "Your credentail is incorrect");
            }
            return View(LoginViewModel);
        }

        #endregion

        #region Validate Authenitcate User

        private bool IsAuthenitcatedUser(string email, string password)
        {
            int count = _userRepository.GetAllUsers().Where(x => x.Email == email && x.Encryptedpassword == Base64Encode(password)).Count();
            return count > 0 ? true : false;
        }

        private static string Base64Encode(string PlainPassword)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(PlainPassword);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        #endregion

        #region Signout Process
        
        public ActionResult SignOut()
        {
            HttpContext.SignOutAsync();
            HttpContext.Session.Remove("username");
            return RedirectToAction("Index", "Login");
        }

        #endregion
    }
}
