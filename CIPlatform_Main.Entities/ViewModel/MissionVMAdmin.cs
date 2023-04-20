using CIPlatform_Main.Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class MissionVMAdmin
	{
		// below field is for mission page
		public string title { get; set; }
		public string? shortDescription { get; set; }
		public string? description { get; set; }
		public long cityId { get; set; }
		public long countryId { get; set; }
		public long themeId { get; set; }
		public string? organizationName { get; set; }

		public string? organizationDetail { get; set; }
		public DateTime? startDate { get; set; }

		public DateTime? endDate { get; set; }
		public DateTime? deadline { get; set; }
	
		public long totalSeats { get; set; }
		public string? availability { get; set; }
		public string? status { get; set; }
		public string missionType { get; set; }

        public List<IFormFile> images { get; set; }

        public List<Mission> MissionList { get; set; }
		public List<MissionMedium> MissionMediaList { get; set; }
		public List<MissionTheme> MissionThemeList { get; set; }
		public List<Skill> SkillList { get; set; }
	}
}
