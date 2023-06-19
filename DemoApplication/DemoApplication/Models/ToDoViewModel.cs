using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using static DemoApplication.BusinessEntity.CommonEnum;

namespace DemoApplication.Models
{
    public class ToDoViewModel
    {
        public int? Id { get; set; }
        [Required]
        [Display(Name = "To-Do items ")]
        public string ToDoItems { get; set; }

        [Display(Name = "Assign Dates ")]
        public DateTime AssignDueDates { get; set; }

        [Display(Name = "Notes/Comments ")]
        public string Comments { get; set; }

        [Display(Name = "Priority ")]
        public PriorityEnum PriorityId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Mark Completed ")]
        public bool IsCompleted { get; set; } = false;
    }

    public class AssignToDoViewModel
    {
        public int ToDoId { get; set; }
        [Display(Name = "Assign User ")]
        public int AssigndUserId { get; set; }
        public List<SelectListItem> UserList { get; set; }
        public AssignToDoViewModel()
        {
            UserList = new List<SelectListItem>();
        }
    }
}
