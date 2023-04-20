using CIPlatform_Main.Entities.Models;
using System;
using System.Collections.Generic;
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
		public string themeTitle { get; set; }
		public byte themeStatus { get; set; }



		// Below field for MissionTheme Page
		public DateTime createdDate { get; set; }



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
