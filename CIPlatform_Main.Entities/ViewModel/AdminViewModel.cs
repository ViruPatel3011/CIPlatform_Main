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
