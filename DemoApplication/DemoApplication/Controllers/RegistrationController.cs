using DemoApplication.BusinessEntity;
using DemoApplication.Models;
using DemoApplication.Repository.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DemoApplication.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserRepository _userRepository;

        public RegistrationController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region User Registration Process

        // GET:Register return view
        [HttpGet]
        public ActionResult UserRegistration()
        {
            return View();
        }

        // Post:Register 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserRegistration(UserRegistrationViewModel RegistrationViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!IsEmailExist(RegistrationViewModel.Email))
                    {
                        Users returnobj = _userRepository.ManageUsers(new Users
                        {
                            UserId = 0,
                            Email = RegistrationViewModel.Email,
                            FirstName = RegistrationViewModel.FirstName,
                            LastName = RegistrationViewModel.LastName,
                            Encryptedpassword = Base64Encode(RegistrationViewModel.Password),
                            Address = RegistrationViewModel.Address
                        });
                        if (returnobj != null)
                        {
                            //Set Login Authentication
                            var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, returnobj.Email), new Claim(ClaimTypes.Role, "User") }, CookieAuthenticationDefaults.AuthenticationScheme);
                            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });
                            HttpContext.Session.SetString("username", returnobj.Email);
                            return RedirectToAction("Index", "User");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Something went wrong please try again later!");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "email already exist please try with diffrent one!");
                    }

                }
                else
                {
                    ModelState.AddModelError("", "Model is Invalid");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
            }
            return View();
        }

        #endregion

        #region Check User Email Exists Or Not Method

        private bool IsEmailExist(string email)
        {
            int count = _userRepository.GetAllUsers().Where(a => a.Email == email).Count();
            return count > 0 ? true : false;
        }

        private static string Base64Encode(string PlainPassword)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(PlainPassword);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        #endregion
    }
}