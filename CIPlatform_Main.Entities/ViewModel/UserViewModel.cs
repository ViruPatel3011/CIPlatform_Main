using CIPlatform_Main.Entities.Models;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

		[Required(ErrorMessage = "Old Password is Required")]
		public string? OldPassword { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		[StringLength(10,MinimumLength =5)]
		public string? Password { get; set; }

		[Required(ErrorMessage = "Confirm Password is Required")]
		[NotMapped]
		[Compare("Password")]
		public string? ConfirmPassword { get; set; }

		public string Avatar { get; set; }
		public long cityId { get; set; }

		public long countryId { get; set; }

		public string? ProfileText { get; set; }

		public string? LinkedInUrl { get; set; }

		public string? Title { get; set; }

		public IEnumerable<City>? Cities { get; set; }
		public IEnumerable<Country>? Countries { get; set; }
		public IEnumerable<Skill>? SkillList { get; set; }
	}
}
