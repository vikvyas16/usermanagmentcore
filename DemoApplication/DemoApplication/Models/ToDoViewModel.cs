using System.ComponentModel.DataAnnotations;

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
        public int UserId { get; set; }
        [Display(Name = "Mark Completed ")]
        public bool IsCompleted { get; set; } = false;
    }
}
