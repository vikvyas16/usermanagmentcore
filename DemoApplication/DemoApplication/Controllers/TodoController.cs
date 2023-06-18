using DemoApplication.BusinessEntity;
using DemoApplication.Models;
using DemoApplication.Repository.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DemoApplication.Controllers
{
    public class TodoController : Controller
    {
        private readonly IToDoRepository _toDoRepository;

        public TodoController(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
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
                    UserId = userId
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
    }
}