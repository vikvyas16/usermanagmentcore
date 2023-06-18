using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace DemoApplication.Models
{
    public class UserRegistrationViewModel
    {
        [Required]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Email: ")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Address: ")]
        public string Address { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
    }
}
