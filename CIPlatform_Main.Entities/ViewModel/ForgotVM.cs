using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
    public class ForgotVM
    {

        [Required(ErrorMessage = "Enter Email Address")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
