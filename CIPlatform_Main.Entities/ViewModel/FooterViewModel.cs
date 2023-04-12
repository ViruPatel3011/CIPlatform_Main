using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class FooterViewModel
	{
		[Required(ErrorMessage = "FirstName is Required")]
		public string? FirstName { get; set; }

		[Required(ErrorMessage = "FirstName is Required")]
		public string? LastName { get; set; }

		public string Email { get; set; }

		// Contact Us
		public string ContactSubject { get; set; } = null!;

		public string ContactMessage { get; set; } = null!;
	}
}
