﻿using CIPlatform_Main.Entities.Models;
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
		[Required(ErrorMessage = "FirstName is Required")]
		public string? FirstName { get; set; }

		[Required(ErrorMessage = "FirstName is Required")]
		public string? LastName { get; set; }

		public string Email { get; set; }

		[Required(ErrorMessage = "You Should Fill This Filed")]
		public string? WhyIVolunteer { get; set; }

		[Required(ErrorMessage = "EmployeeId is Required")]
		public string? EmployeeId { get; set; }

		//[Required(ErrorMessage = "ManagerDetail is Required")]
		public string? ManagerDetail { get; set; }

		[Required(ErrorMessage = "Department is Required")]
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

		[Required(ErrorMessage = "ProfileText is Required")]
		public string? ProfileText { get; set; }


		[Required(ErrorMessage = "LinkedInUrl is Required")]
		public string? LinkedInUrl { get; set; }


		[Required(ErrorMessage = "Title is Required")]
		public string? Title { get; set; }

		// Contact Us
		public string ContactSubject { get; set; } = null!;

		public string ContactMessage { get; set; } = null!;


		public IEnumerable<User>? UserData { get; set; }
		public IEnumerable<City>? Cities { get; set; }
		public IEnumerable<Country>? Countries { get; set; }
		public IEnumerable<Skill>? SkillList { get; set; }
		public IEnumerable<UserSkill>? UserSkills { get; set; }

		public Country? CountryName { get; set; }
	}
}
