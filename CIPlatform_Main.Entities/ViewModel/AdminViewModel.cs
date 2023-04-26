using CIPlatform_Main.Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class AdminViewModel
	{
		// Below field for User

		[Required(ErrorMessage = "First Name is required")]
		public string? FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required")]
		public string? LastName { get; set; }

		[Required(ErrorMessage = "Email is required")]
		public string UEmail { get; set; } = null!;

		[Required(ErrorMessage = "Password is required")]
		[DataType(DataType.Password)]
		[StringLength(10, MinimumLength = 6)]
		[RegularExpression("([a-z]|[A-Z]|[0-9]|[\\W]){4}[a-zA-Z0-9\\W]{3,11}", ErrorMessage = "Invalid password format")]
		public string UPassword { get; set; } = null!;

		[RegularExpression("^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
		[Required(ErrorMessage = "PhoneNumber is required")]
		public string PhoneNumber { get; set; } = null!;

		[Required(ErrorMessage = "Avtar is required")]
		public string? Avatar { get; set; }

		[Required(ErrorMessage = "Employee Id is required")]
		public string? EmployeeId { get; set; }

		[Required(ErrorMessage = "Department is required")]
		public string? Department { get; set; }

		[Required(ErrorMessage = "status is required")]
		public string Status { get; set; } = null!;




		// Below field is for AdminLogin
		[Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }


        // Below field is for CMS Page
        public string CMSTitle { get;set; }	
		public string CMSDescrition { get;set; }	
		public string CMSSlug { get;set; }	
		public string CMSStatus { get;set; }	



		public long storyId { get; set; }
		// below field is for mission page
		public string title { get; set; }
		public string? status { get; set; }
		public string missionType { get; set; }

		public DateTime? startDate { get; set; }
		public DateTime? endDate { get; set; }



        // below field is for mission Theme page
        [Required(ErrorMessage = "ThemeTitle Is Required")]
        public string themeTitle { get; set; }

        [Required(ErrorMessage = "ThemeStatus Is Required")]
        public byte themeStatus { get; set; }



		// Below field for MissionTheme Page

		public DateTime createdDate { get; set; }

		// Below field for MissionApplication Page
		[Required(ErrorMessage = "MissionId Is Required")]
		public long missionId;

		[Required(ErrorMessage = "UserId Is Required")]
		public long userId;
			
		[Required(ErrorMessage = "ApprovalStatus Is Required")]
		public string ApprovalStatus;

		[Required(ErrorMessage = "createdAt Is Required")]
		public DateTime createdat;

		[Required(ErrorMessage = "appliedAt Is Required")]
		public DateTime appliedat;


		// Below field for MissionSkill Page

		[Required(ErrorMessage = "skillName Is Required")]
		public string skillName;

		[Required(ErrorMessage = "skillDate Is Required")]
		public DateTime skillDate;


		// All the Required List 

		public List<User> UserList { get; set; }
		public List<CmsPage> CMSPageList { get; set; }
		public List<Mission> MissionList { get; set; }

		//public List<MissionApplication> MissionAppList { get; set; }
		public List<MissionTheme> MissionThemeList { get; set; }
		public List<MissionApplication> MissionApplicationList { get; set; }
		public List<Skill> SkillsList { get; set; }
		public List<Story> StoryList { get; set; }
		public List<Banner> BannerList { get; set; }



	}
}
