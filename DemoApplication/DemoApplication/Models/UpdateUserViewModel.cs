using System.ComponentModel.DataAnnotations;

namespace DemoApplication.Models
{
    public class UpdateUserViewModel
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Address: ")]
        public string Address { get; set; }
    }
}
