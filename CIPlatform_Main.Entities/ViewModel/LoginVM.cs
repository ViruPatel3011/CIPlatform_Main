using CIPlatform_Main.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "Email is Required")]
	
		public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }

        public List<Banner> BannerList { get; set; }
    }
}
