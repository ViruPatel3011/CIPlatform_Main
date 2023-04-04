using CIPlatform_Main.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class UserViewModel
	{
		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public string Email { get; set; } 
		public string? WhyIVolunteer { get; set; }

		public string? EmployeeId { get; set; }

		public string? Department { get; set; }
		public string Password { get; set; } = null!;


		public long cityId { get; set; }

		public long countryId { get; set; }

		public string? ProfileText { get; set; }

		public string? LinkedInUrl { get; set; }

		public string? Title { get; set; }

		public IEnumerable<City>? Cities { get; set; }
		public IEnumerable<Country>? Countries { get; set; }
	}
}
