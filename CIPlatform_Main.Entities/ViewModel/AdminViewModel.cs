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

		// below field is for mission page
		public string title { get; set; }
		public string? status { get; set; }

		public DateTime? startDate { get; set; }

		// below field is for mission Theme page
		public string themeTitle { get; set; }
		public byte themeStatus { get; set; }

		public DateTime createdDate { get; set; }
		public DateTime? endDate { get; set; }
		public string missionType { get; set; }

		public List<User> UserList { get; set; }
		public List<CmsPage> CMSPageList { get; set; }
		public List<Mission> MissionList { get; set; }
		public List<MissionApplication> MissionAppList { get; set; }
		public List<MissionTheme> MissionThemeList { get; set; }


	}
}
