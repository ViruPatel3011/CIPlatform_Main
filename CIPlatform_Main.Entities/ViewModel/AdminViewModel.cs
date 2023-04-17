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

		public string title { get; set; }
		public string? status { get; set; }

		public DateTime? startDate { get; set; }
		public DateTime? endDate { get; set; }
		public string missionType { get; set; }

		public List<User> UserList { get; set; }
		public List<CmsPage> CMSPageList { get; set; }
		public List<Mission> MissionList { get; set; }
		public List<MissionApplication> MissionAppList { get; set; }
		public List<MissionTheme> MissionThemeList { get; set; }


	}
}
