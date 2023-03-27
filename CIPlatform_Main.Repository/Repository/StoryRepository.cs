using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Repository
{
	public class StoryRepository:IStoryRepository
	{
		private readonly CiPlatformContext _ciPlatformContext;

		public StoryRepository(CiPlatformContext ciPlatformContext)
		{
			_ciPlatformContext = ciPlatformContext;
		}

		public storyListingVM getStoryDetail(storyListingVM svm)
		{
			storyListingVM tsm = new storyListingVM()
			{
				stories = _ciPlatformContext.Stories.ToList(),
				users = _ciPlatformContext.Users.ToList(),
				missionTheme = _ciPlatformContext.MissionThemes.ToList(),
				allCities = _ciPlatformContext.Cities.ToList(),
				allCountries = _ciPlatformContext.Countries.ToList(),
				skills = _ciPlatformContext.Skills.ToList(),
				allMissionList = _ciPlatformContext.Missions.ToList(),
			};
			return tsm;
		}

		public bool getDataForStoryTable(int MissionId, string StoryTitle, string StoryText, DateTime StoryDate, int userId)
		{
			Story str = new Story();

			str.MissionId = MissionId;
			str.Title = StoryTitle;
			str.PublishedAt = StoryDate;
			str.Description = StoryText;
			str.UserId = userId;

			_ciPlatformContext.Stories.Add(str);
			_ciPlatformContext.SaveChanges();
			return true;
		}


	}
}
