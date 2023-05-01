using CIPlatform_Main.Entities.Models;
using Microsoft.AspNetCore.Http;
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
		[StringLength(20, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_])[a-zA-Z\d\W_]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
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
		public long cmsId { get;set; }

		public long loadCMsid { get; set; }



		public long storyId { get; set; }
		// below field is for mission page




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



		// banner Page Field
		public long BannerId { get; set; }

		[Required(ErrorMessage = "Image Is Required")]
		public IFormFile? bannerImage { get; set; }

		[Required(ErrorMessage = "BannerText Is Required")]
		public string? bannerText { get; set; }

		[Required(ErrorMessage = "SortOrder Is Required")]
		public int? sortOrder { get; set; }

		[Required(ErrorMessage = "CreatedAt Is Required")]
		public DateTime CreatedAt { get; set; }

		public IFormFile? bannerImageE { get; set; }




		// below field is for mission page

		//public long missionId { get; set; }
		public long mId { get; set; }
		public string title { get; set; }
		public string? status { get; set; }
		public string missionType { get; set; }

		public DateTime? startDate { get; set; }
		public DateTime? endDate { get; set; }

	

		[Required(ErrorMessage = "MissionShortDescription Is Required")]
		public string? shortDescription { get; set; }

		[Required(ErrorMessage = "MissionDescription Is Required")]
		public string? description { get; set; }

		//[Required(ErrorMessage = "City Is Required")]
		public long cityId { get; set; }

		//[Required(ErrorMessage = "Country Is Required")]
		public long countryId { get; set; }

		[Required(ErrorMessage = "MissionTheme Is Required")]
		public long themeId { get; set; }

		[Required(ErrorMessage = "Organizatiion Name Is Required")]
		public string? organizationName { get; set; }

		[Required(ErrorMessage = "Organization Detail Is Required")]

		public string? organizationDetail { get; set; }

	

		//[Required(ErrorMessage = "Deadline Is Required")]
		public DateTime? deadline { get; set; }

		//[Required(ErrorMessage = "Total Seats Is Required")]

		public long totalSeats { get; set; }

		[Required(ErrorMessage = "Availability Is Required")]
		public string? availability { get; set; }

		
	

		[Required(ErrorMessage = "Please Select Images")]

		public List<IFormFile> images { get; set; }
		public List<string> editMissionImages { get; set; }

		[Required(ErrorMessage = "Please Select Documents")]
		public List<IFormFile> Documents { get; set; }
		public List<string> editMissionDocuments { get; set; }

		public List<string> imagepaths { get; set; }

	
		public List<MissionMedium> MissionMediaList { get; set; }
	
		public List<Skill> SkillList { get; set; }
		public List<MissionSkill> MissionSkillList { get; set; }
		public List<Country> CountryList { get; set; }
		public List<City> CityList { get; set; }
	}
}
