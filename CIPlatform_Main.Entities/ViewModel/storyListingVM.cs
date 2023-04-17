using CIPlatform_Main.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
	public class storyListingVM
	{
		public string Avatar {  get; set; }
		public IEnumerable<User>? users { get; set; }
		public IEnumerable<MissionTheme>? missionTheme { get; set; }
		public IEnumerable<Mission>? allMissionList { get; set; }
		public IEnumerable<City>? allCities { get; set; }
		public IEnumerable<Country>? allCountries { get; set; }
		public IEnumerable<Skill>? skills { get; set; }

		public IEnumerable<Story>? stories { get; set; }
        public IEnumerable<StoryMedium>? storyMedia { get; set; }
        public string search { get; set; }
		public string missionName { get; set; }
		public string storyTitle { get; set; }
		public string storyDescription { get; set; }
		public DateTime dateAndTime { get; set; }
		public int Viewscount { get; set; }

		//public int views { get; set; }

		public int currentPage { get; set; }
		public int totalRecord{ get; set; }

   
    }
}
