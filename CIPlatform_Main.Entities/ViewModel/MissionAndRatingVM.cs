using CIPlatform_Main.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class MissionAndRatingVM
	{
		public Mission MissionDetail { get; set; }
		/* public MissionDocument MissionDocuments{ get; set; }*/
		public Mission MissionThemeId { get; set; }
		public MissionTheme MissionTheme { get; set; }

		public string currentTime { get; set; }
		public int userId { get; set; }
		public int missionId { get; set; }

		public string Avatar { get; set; }
		public int currentPage { get; set; }
		public int totalrecord { get; set; }

		public int missionRate { get; set; }
		public int? userRate { get; set; }

		

		public int overallRating { get; set; }
		public int numberOfVolunteers { get; set; }

		public int RatedVolunteers { get; set; }

		public bool Favourite { get; set; }
		public City City { get; set; }

		public int? Aimed { get; set; }
		public int GoalValue { get; set; }
		public GoalMission GoalMission { get; set; }

		/* public int currentPage { get; set; }

         public int totalRecords {  get; set; }  */
		public string search { get; set; }
		public IEnumerable<Country>? Countries { get; set; }
		public IEnumerable<City>? Cities { get; set; }
		public IEnumerable<Mission>? Missions { get; set; }
		public IEnumerable<MissionDocument>? MissionDocuments { get; set; }
		public IEnumerable<MissionTheme>? MissionThemes { get; set; }
		public IEnumerable<MissionSkill>? MissionSkills { get; set; }
		public IEnumerable<Skill>? Skills { get; set; }
		public IEnumerable<GoalMission>? GoalMissions { get; set; }

		public IEnumerable<Mission>? RelatedMissions { get; set; }
		public IEnumerable<MissionRating>? MissionRatings { get; set; }
		public IEnumerable<MissionApplication>? missionApplications { get; set; }
		public IEnumerable<FavoriteMission>? FavoriteMissionList { get; set; }
		public IEnumerable<User>? UserForRecommendation { get; set; }
		public IEnumerable<User>? UserData { get; set; }
		public IEnumerable<Comment>? comments { get; set; }
		public List<MissionMedium>? MissionMedia { get; set; }
		public IEnumerable<Timesheet>? TimeSheetList { get; set; }
		public IEnumerable<Admin>? AdminList { get; set; }
	}
}
