﻿using CIPlatform_Main.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Entities.ViewModel
{
    public   class storyDetailVM
    {
        public IEnumerable<User>? UserData { get; set; }
        public IEnumerable<MissionTheme>? MissionThemes { get; set; }
        public IEnumerable<Mission>? MissionList { get; set; }
        public IEnumerable<City>? CityList { get; set; }
        public IEnumerable<Country>? CountryList { get; set; }
        public IEnumerable<Skill>? SkillList { get; set; }

        public IEnumerable<Story>? Stories { get; set; }
        
      
        public IEnumerable<StoryMedium>? storyMedia { get; set; }
        public string search { get; set; }
        public string missionName { get; set; }
        public string storyTitle { get; set; }
        public string storyDescription { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
       
        public string Avatar { get; set; }

      

        public long MissionId { get; set; }
        public long UserId { get; set; }
		public int Viewscount { get; set; }
		//public int views { get; set; }


		public string WhyIVolunteered { get; set; }   
        public DateTime dateAndTime { get; set; }

		public IEnumerable<MessageTable>? NotificationMessage { get; set; }
	}
}
