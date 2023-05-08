using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Repository
{
    public class LandingPageRepository:ILandingPage
    {
		private readonly CiPlatformContext _ciPlatformContext;

		public LandingPageRepository(CiPlatformContext ciPlatformContext)
		{
			_ciPlatformContext = ciPlatformContext;
		}

		public LandingPageVM LandingPageList()
		{
			LandingPageVM landingPageData= new LandingPageVM();

			List<User> users = GetUsers();
			landingPageData.UserData = users;
			
			List<Country> countries = GetCountries();
			landingPageData.Countries = countries;

			List<City> cities = GetCities();
			landingPageData.Cities = cities;

			List<Mission> missions = GetMissions();
			landingPageData.Missions = missions;

			List<MissionTheme> missionTheme = GetMissionThemes();
			landingPageData.MissionThemes = missionTheme;

			List<MissionSkill> missionSkills = GetMissionSkills();
			landingPageData.MissionSkills = missionSkills;

			List<Skill> skill = GetSkills();
			landingPageData.Skills = skill;
			
			List<MissionApplication> mApp = GetMissionApplication();
			landingPageData.missionApplications = mApp;
			
			List<GoalMission> goalMission = GetGoalMission();
			landingPageData.GoalMissions = goalMission;
			
			List<Admin> admin = GetAdminList();
			landingPageData.AdminList = admin;

			return landingPageData;


			
		}
		public LandingPageVM LandingPage(string[]? country, string[]? city, string[]? themes, string[]? skills, string? sortVal, string? exploreVal, string? search, int pg = 1)
		{
			LandingPageVM landingPage = new LandingPageVM();

			List<User> users = GetUsers();
			landingPage.UserData = users;
			
			List<Country> countries = GetCountries();
			landingPage.Countries = countries;

			List<City> cities = GetCities();
			landingPage.Cities = cities;

			List<Mission> missions = GetMissions();
			landingPage.Missions = missions;

			List<MissionTheme> missionTheme = GetMissionThemes();
			landingPage.MissionThemes = missionTheme;
			
			List<MissionApplication> missionApp = GetMissionApplication();
			landingPage.missionApplications = missionApp;

			List<MissionSkill> missionSkills = GetMissionSkills();
			landingPage.MissionSkills = missionSkills;

			List<Skill> skill = GetSkills();
			landingPage.Skills = skill;
			
			List<GoalMission> goalMissionList = GetGoalMission();
			landingPage.GoalMissions = goalMissionList;

			List<GoalMission> goalMission = GetGoalValue();
			landingPage.GoalMissions = goalMission;
			
			List<MissionRating> missionRating = GetMissionRating();
			landingPage.MissionRatings = missionRating;

			List<FavoriteMission> favouriteMission = GetFavouriteMissions();
			landingPage.FavoriteMissionList = favouriteMission;

			List<User> userData = GetUserData();
			landingPage.UserData = userData;
			
			List<MissionMedium> missionMedia = GetMissionMedia();
			landingPage.MissionMedia = missionMedia;
			
			List<Timesheet> missionTimsheet = GetTimesheetData();
			landingPage.TimeSheetList = missionTimsheet;
			

			

			return landingPage;


		}

		public List<User> GetUsers()
		{
			List<User> users = _ciPlatformContext.Users.ToList();
			return users;
		}
		
		public List<Country> GetCountries()
		{
			List<Country> countries = _ciPlatformContext.Countries.ToList();
			return countries;
		}
		public List<City> GetCities()
		{
			List<City> cities = _ciPlatformContext.Cities.ToList();
			return cities;
		}

		public List<Mission> GetMissions()
		{
			List<Mission> missions = _ciPlatformContext.Missions.Where(mission => mission.Status == "Active").ToList();

			return missions;
		}
		public List<MissionMedium> GetMissionMedia()
		{
			List<MissionMedium> missions = _ciPlatformContext.MissionMedia.ToList();

			return missions;
		}
		public List<MissionTheme> GetMissionThemes()
		{
			List<MissionTheme> missionThemes = _ciPlatformContext.MissionThemes.ToList();
			return missionThemes;
		}
		
		public List<MissionApplication> GetMissionApplication()
		{
			List<MissionApplication> missionApp = _ciPlatformContext.MissionApplications.ToList();
			return missionApp;
		}
		public List<MissionSkill> GetMissionSkills()
		{
			List<MissionSkill> missionSkiils = _ciPlatformContext.MissionSkills.ToList();
			return missionSkiils;
		}
		public List<Skill> GetSkills()
		{
			List<Skill> skils = _ciPlatformContext.Skills.ToList();
			return skils;
		}
		
		public List<GoalMission> GetGoalMission()
		{
			List<GoalMission> goalMissions = _ciPlatformContext.GoalMissions.ToList();
			return goalMissions;
		}
		public List<GoalMission> GetGoalValue()
		{
			List<GoalMission> goalMissions = _ciPlatformContext.GoalMissions.ToList();
			return goalMissions;
		}
		public List<MissionRating> GetMissionRating()
		{
			List<MissionRating> missionRatings = _ciPlatformContext.MissionRatings.ToList();
			return missionRatings;
		}
		
		public List<FavoriteMission> GetFavouriteMissions()
		{
			List<FavoriteMission> favoriteMissions = _ciPlatformContext.FavoriteMissions.ToList();
			return favoriteMissions;
		}
		
		public List<User> GetUserData()
		{
			List<User> getUser = _ciPlatformContext.Users.ToList();
			return getUser;
		}
		public List<Admin> GetAdminList()
		{
			List<Admin> getAdmin = _ciPlatformContext.Admins.ToList();
			return getAdmin;
		}
		
		public List<Timesheet> GetTimesheetData()
		{
			List<Timesheet> timeSheet = _ciPlatformContext.Timesheets.ToList();
			return timeSheet;
		}
	}
}
