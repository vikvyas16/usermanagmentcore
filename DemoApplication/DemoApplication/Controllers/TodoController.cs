using DemoApplication.BusinessEntity;
using DemoApplication.Filters;
using DemoApplication.Models;
using DemoApplication.Repository.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DemoApplication.Controllers
{
    [TypeFilter(typeof(CustomAuthorizationFilterAttribute))]
    public class TodoController : Controller
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IUserRepository _userRepository;

        public TodoController(IToDoRepository toDoRepository, IUserRepository userRepository)
        {
            _toDoRepository = toDoRepository;
            _userRepository = userRepository;
        }

        #region Get Current User Todo List

        public IActionResult Index()
        {
            List<ToDoViewModel> list = new List<ToDoViewModel>();
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            List<ToDo> items = _toDoRepository.GetAllToDoItems().Where(x => x.UserId == userId).ToList();
            foreach (var item in items)
            {
                ToDoViewModel model = new ToDoViewModel();
                model.Id = item.Id;
                model.ToDoItems = item.ToDoItems;
                model.AssignDueDates = item.AssignDueDates;
                model.Comments = item.Comments;
                model.IsCompleted = item.IsCompleted;
                model.UserId = item.UserId;
                model.PriorityId = (CommonEnum.PriorityEnum)item.PriorityId;
                list.Add(model);
            }
            return View(list);
        }

        #endregion

        #region Add/Update/Delete TODO List

        [HttpGet]
        public ActionResult<ToDoViewModel> AddEdit(int id)
        {
            var toDoItemModel = _toDoRepository.GetToDoItemByToDoId(id);
            if (toDoItemModel == null)
            {
                return View(new ToDoViewModel());
            }
            else
            {
                ToDoViewModel model = new ToDoViewModel();
                model.Id = toDoItemModel.Id;
                model.ToDoItems = toDoItemModel.ToDoItems;
                model.AssignDueDates = toDoItemModel.AssignDueDates;
                model.Comments = toDoItemModel.Comments;
                model.PriorityId = (CommonEnum.PriorityEnum)toDoItemModel.PriorityId;
                model.IsCompleted = toDoItemModel.IsCompleted;
                model.UserId = toDoItemModel.UserId;
                return View(model);
            }            
        }

        [HttpPost]
        public IActionResult ManageToDo(ToDoViewModel model)
        {
            if (ModelState.IsValid)
            {
                int userId = Convert.ToInt32(HttpContext.Session.GetString("userid"));
                ToDo returnobj = _toDoRepository.ManageToDo(new ToDo
                {
                    Id = Convert.ToInt32(model.Id > 0 ? model.Id : 0),
                    ToDoItems = model.ToDoItems,
                    AssignDueDates = model.AssignDueDates,
                    Comments = model.Comments,
                    IsCompleted = model.IsCompleted,
                    UserId = userId,
                    PriorityId = (int)model.PriorityId
                });
                if (returnobj != null)
                {
                    return RedirectToAction("Index", "Todo");
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
            return View(model);
        }

        #endregion

        #region Assign ToDo

        [HttpGet]
        public ActionResult<AssignToDoViewModel> AssignTodo(int id)
        {
            AssignToDoViewModel model = new AssignToDoViewModel();
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userid"));
            model.ToDoId = id;
            model.UserList = _userRepository.GetAllUsers().Where(x => x.UserId != userId).Select(x => new SelectListItem { Text = x.FirstName + " " + x.LastName, Value = Convert.ToString(x.UserId) }).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult<AssignToDoViewModel> AssignTodo(AssignToDoViewModel model)
        {
            if (ModelState.IsValid)
            {
                _toDoRepository.AssignedToDoList(model.ToDoId, model.AssigndUserId);
                ViewBag.Message = "Assigned To-Do List to User Successfully..";
                return RedirectToAction("Index", "Todo");
            }
            else
            {
                ModelState.AddModelError("", "Model is Invalid");
            }
            return View(model);            
        }

        #endregion
    }
}