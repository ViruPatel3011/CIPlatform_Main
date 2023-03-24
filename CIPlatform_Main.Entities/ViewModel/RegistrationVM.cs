using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
    public class RegistrationVM
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;



        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6)]
        [RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Invalid password format")]
        public string Password { get; set; } = null!;


        [Required(ErrorMessage = "ConfirmPassword is required")]
        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = null!;


        [RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        [Required(ErrorMessage = "Phone Number is required")]
        public string PhoneNumber { get; set; }
    }
}
