using DemoApplication.BusinessEntity;
using DemoApplication.Filters;
using DemoApplication.Models;
using DemoApplication.Repository.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Controllers
{
    [TypeFilter(typeof(CustomAuthorizationFilterAttribute))]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        #region If Success Login when This Method Calling

        //Only able access this Action method when User Log in
       // [Authorize]
        public ActionResult Index()
        {
            string username = HttpContext.Session.GetString("username");
            if (!string.IsNullOrEmpty(username))
            {
                Users user = _userRepository.GetAllUsers().Where(x => x.Email == username).FirstOrDefault();
                HttpContext.Session.SetString("userid", Convert.ToString(user.UserId));
                ViewBag.UserId = user.UserId;
                ViewBag.DisplayName = user.FirstName + " " + user.LastName;

                DashboardViewModel dm = _userRepository.GetDashboardCountByUserId(user.UserId);

                ViewBag.ToDoCreated = dm.CreatedTodoCount;
                ViewBag.AssignedToDo = dm.AssignedTodoCount;
                ViewBag.Completed = dm.CompletedTodoCount;
            }
            else
            {
                ViewBag.Message = "Unauthorize User..! Not accessible this page";
            }
            return View();
        }

        #endregion

        #region Update User

        public ActionResult Edit(int id)
        {
            Users user = _userRepository.GetUserByUserId(id);
            if (user != null)
            {
                return View(new UpdateUserViewModel()
                {
                    UserId = user.UserId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Address = user.Address
                });
            }
            return View();
        }

        // Post:Register 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UpdateUserViewModel editViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Users returnobj = _userRepository.ManageUsers(new Users
                    {
                        UserId = editViewModel.UserId,
                        FirstName = editViewModel.FirstName,
                        LastName = editViewModel.LastName,
                        Address = editViewModel.Address
                    });
                    if (returnobj != null)
                    {
                        ViewBag.Message = "User  Updated Successfully..";
                        return View(editViewModel);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Something went wrong please try again later!");
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

        #region Get User Notification

        [HttpGet]
        public JsonResult GetNotifications(int userId)
        {
            return Json(_userRepository.GetNotificationByUserId(userId));
        }


        #endregion

        #region GetReadNotifications

        [HttpGet]
        public JsonResult GetReadNotifications(int notificationId)
        {
            return Json(_userRepository.IsReadNotification(notificationId));
        }
        #endregion
    }
}