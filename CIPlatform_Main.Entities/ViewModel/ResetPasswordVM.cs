using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
    public class ResetPasswordVM
    {
        public string Token { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        [NotMapped]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
