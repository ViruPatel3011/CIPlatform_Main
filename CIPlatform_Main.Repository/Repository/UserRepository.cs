using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly CiPlatformContext _ciPlatformContext;

		public UserRepository(CiPlatformContext ciPlatformContext)
		{
			_ciPlatformContext = ciPlatformContext;
		}

		public UserViewModel getUserData(int uid)
		{
			var loginUser = _ciPlatformContext.Users.Where(x => x.UserId == uid).FirstOrDefault();
			var cityList = _ciPlatformContext.Cities.ToList();
			var countryList = _ciPlatformContext.Countries.ToList();
			var skiilsList = _ciPlatformContext.Skills.ToList();
			var userList=_ciPlatformContext.Users.ToList();
			var usesSkils = _ciPlatformContext.UserSkills.Where(x => x.UserId == uid).ToList();
			Country countryName= _ciPlatformContext.Countries.Where(c => c.CountryId ==loginUser.CountryId).FirstOrDefault();

			UserViewModel userViewModel = new UserViewModel()
			{
				FirstName = loginUser.FirstName,
				LastName = loginUser.LastName,
				EmployeeId = loginUser.EmployeeId,
				Title = loginUser.Title,
				Department = loginUser.Department,
				ProfileText = loginUser.ProfileText,
				WhyIVolunteer = loginUser.WhyIVolunteer,
				Password = loginUser.Password,
				LinkedInUrl = loginUser.LinkedInUrl,
				Cities = cityList,
				Countries = countryList,
				SkillList=skiilsList,
				Avatar=loginUser.Avatar,
				ManagerDetail=loginUser.Manager,
				CountryName = countryName,
				UserData=userList,
				UserSkills= usesSkils,

			};
			return userViewModel;
		}



		public void SaveUserProfile(UserViewModel userView, int uid)
		{
			var alreadyExitUser = _ciPlatformContext.Users.Where(x => x.UserId == Convert.ToInt32(uid)).FirstOrDefault();
		
			if(userView.countryId == 0 && userView.cityId == 0)
			{
				
				alreadyExitUser.FirstName = userView.FirstName;
				alreadyExitUser.LastName = userView.LastName;
				alreadyExitUser.EmployeeId = userView.EmployeeId;
				alreadyExitUser.Title = userView.Title;
				alreadyExitUser.Department = userView.Department;
				alreadyExitUser.ProfileText = userView.ProfileText;
				alreadyExitUser.WhyIVolunteer = userView.WhyIVolunteer;
				alreadyExitUser.LinkedInUrl = userView.LinkedInUrl;
				alreadyExitUser.Manager = userView.ManagerDetail;
			}
			else
			{
				alreadyExitUser.FirstName = userView.FirstName;
				alreadyExitUser.LastName = userView.LastName;
				alreadyExitUser.EmployeeId = userView.EmployeeId;
				alreadyExitUser.Title = userView.Title;
				alreadyExitUser.Department = userView.Department;
				alreadyExitUser.ProfileText = userView.ProfileText;
				alreadyExitUser.WhyIVolunteer = userView.WhyIVolunteer;
				alreadyExitUser.LinkedInUrl = userView.LinkedInUrl;
				alreadyExitUser.Manager = userView.ManagerDetail;
				alreadyExitUser.CityId = userView.cityId;
				alreadyExitUser.CountryId = userView.countryId;

			}
				
				
				_ciPlatformContext.SaveChanges();

		}

		public int AddSkills(long[] SkillArray, int uid)
		{
			List<int> skillIds = new List<int>();

			var skills = _ciPlatformContext.Skills.Where(s => SkillArray.Contains(s.SkillId)).ToList();

			foreach (var skill in skills)
			{
				skillIds.Add((int)skill.SkillId);
			}

			var existingUserSkills = _ciPlatformContext.UserSkills.Where(us => us.UserId == uid);
			_ciPlatformContext.UserSkills.RemoveRange(existingUserSkills);

			foreach (int skillid in skillIds)
			{
				var usrskill = new UserSkill()
				{
					SkillId = skillid,
					UserId = uid,
					CreatedAt = DateTime.Now
				};
				_ciPlatformContext.UserSkills.Add(usrskill);
			}
			_ciPlatformContext.SaveChanges();

			return 1;
		}







		public string changeAvatar(string image, int uid)
		{
			var userdetail = _ciPlatformContext.Users.Where(x => x.UserId == uid).FirstOrDefault();
			userdetail.Avatar = image;
			_ciPlatformContext.Update(userdetail);
			_ciPlatformContext.SaveChanges();
			return "Success";
		}

		public int ChangeUserPassword(int uid, UserViewModel userViewModel)
		{
			var checkUser = _ciPlatformContext.Users.Where(x => x.UserId == uid).FirstOrDefault();
			if (checkUser.Password == userViewModel.OldPassword)
			{
				if (userViewModel.OldPassword == userViewModel.Password)
				{
					return 0;
				}
				else
				{
					checkUser.Password = userViewModel.ConfirmPassword;
					_ciPlatformContext.SaveChanges();
					return 1;
				}


			}
			else
			{
				return -1;
			}
			
		}

		public List<Mission> getTimeBaseMission(int uid)
		{

			var missionApplied = _ciPlatformContext.MissionApplications.Where(x => x.UserId == uid).Select(x => x.MissionId);
			
			return _ciPlatformContext.Missions.Where(x => missionApplied.Contains(x.MissionId) && x.MissionType == "Time").OrderBy(x => x.Title).ToList(); ;
		}
		public List<Mission> getGoalBaseMission(int uid)
		{

			var missionApplied = _ciPlatformContext.MissionApplications.Where(x => x.UserId == uid).Select(x => x.MissionId);
			return _ciPlatformContext.Missions.Where(x => missionApplied.Contains(x.MissionId) && x.MissionType == "Goal").OrderBy(x => x.Title).ToList(); ;
		}

		public List<Timesheet> getTimeBasedSheet(int uid)
		{
			var sheetList=_ciPlatformContext.Timesheets.Where(x=>x.Mission.MissionType=="Time" && x.UserId == uid).ToList();
			return sheetList;

		}
		public List<Timesheet> getGoalBasedSheet(int uid)
		{
			var sheetList=_ciPlatformContext.Timesheets.Where(x=>x.Mission.MissionType=="Goal" && x.UserId == uid).ToList();
			return sheetList;

		}


		// Add time based Data on database
		public void AddTimeSheetData(VolTimeSheetVM volTime,int uid)
		{
			DateTime dateTime=DateTime.Now;
			var userVolunteeredDate = _ciPlatformContext.MissionApplications.Where(x => x.UserId == uid && x.MissionId == volTime.MissionId).Select(x => x.AppliedAt).FirstOrDefault();

			var hour = volTime.hours;
			var minute = volTime.minutes;
			var time = new TimeOnly(hour, minute, 0);


			// This condition is for not added data fro same user twice
			//var alreadyDataExist = _ciPlatformContext.Timesheets.Where(x => x.MissionId == volTime.MissionId).FirstOrDefault();
			//if (alreadyDataExist == null)
			//{
				Timesheet timesheet = new Timesheet()
				{
					MissionId = volTime.MissionId,
					UserId = uid,
					DateVolunteered = userVolunteeredDate,
					Status = "Pending",
					CreatedAt = DateTime.Now,
					TimesheetTime = time,
					Notes = volTime.missionDetail,
					//Action=volTime.Action


				};
				_ciPlatformContext.Add(timesheet);
				_ciPlatformContext.SaveChanges();
			//	return true;
			//}
			//else
			//{
			//	return false;
			//}


		}


		// Add Goal based Data on database
		public void AddGoalBaseData(VolTimeSheetVM volTime,int uid)
		{
			DateTime dateTime=DateTime.Now;
			var userVolunteeredDate = _ciPlatformContext.MissionApplications.Where(x => x.UserId == uid && x.MissionId == volTime.MissionId).Select(x => x.AppliedAt).FirstOrDefault();

			//var hour = volTime.Hours;
			//var minute = volTime.Minutes;
			//var time = new TimeOnly(hour, minute, 0);

			// This condition is for not added data fro same user twice
			//var alreadyDataExist = _ciPlatformContext.Timesheets.Where(x => x.MissionId == volTime.MissionId).FirstOrDefault();
			//if (alreadyDataExist == null)
			//{
			Timesheet timesheet = new Timesheet()
				{
					MissionId = volTime.MissionId,
					UserId = uid,
					DateVolunteered = userVolunteeredDate,
					Status = "Pending",
					CreatedAt = DateTime.Now,
					//TimesheetTime=time,
					Notes = volTime.missionDetail,
					Action = volTime.action


				};
				_ciPlatformContext.Add(timesheet);
				_ciPlatformContext.SaveChanges();
				//return true;
			//}
			//else
			//{
			//	return false;	
			//}
			


		}





		public List<Country> GetCountryList()
		{
			var countryList = _ciPlatformContext.Countries.ToList();
			return countryList;
		}
		public List<City> GetCityList(int id)
		{
			var cityList = _ciPlatformContext.Cities.Where(x=>x.CountryId==id).ToList();
			return cityList;
		}


		// Get Time data for Edit on Icon click
		public Timesheet getDataForEditSectionForTimeBase(long tId, long uid)
		{
			var data = _ciPlatformContext.Timesheets.FirstOrDefault(x => x.TimesheetId== tId && x.UserId == uid);
			return data;
		}


		// Get Goal data for Edit on Icon click
		public Timesheet getDataForEditSectionForGoalBase(long goalBasedId, long uid)
		{
			var data = _ciPlatformContext.Timesheets.FirstOrDefault(x => x.TimesheetId == goalBasedId && x.UserId == uid);
			return data;
		}


		public string getMissionNameForEditSection(long tId)
		{
			var missionName=_ciPlatformContext.Timesheets.Where(x=>x.TimesheetId==tId).Select(x=>x.Mission.Title).FirstOrDefault();
			return missionName;
		}

		//edit data for Time base mission on Edit Button
		public void editDataForTimeMission(VolTimeSheetVM vtvm, int tId, long uid)
		{
			var data = _ciPlatformContext.Timesheets.Where(x => x.TimesheetId == tId && x.UserId == uid).FirstOrDefault();
			var hour = vtvm.hours;
			var minute = vtvm.minutes;
			var timeOnly = new TimeOnly(hour, minute, 0);
			data.TimesheetTime = timeOnly;
			data.Notes = vtvm.missionDetail;
			_ciPlatformContext.SaveChanges();
		}


		//edit data for goal base mission on Edit Button
		public void editDataForGoalMission(VolTimeSheetVM vtvm, long timeSheetId, long uid)
		{
			var data = _ciPlatformContext.Timesheets.Where(x => x.TimesheetId == timeSheetId && x.UserId == uid).FirstOrDefault();
			data.Notes = vtvm.missionDetail;
			_ciPlatformContext.SaveChanges();
		}


		// Delete data from timesheet
		public bool removeTimeBasedData(int tId, int uid)
		{
			var entry=_ciPlatformContext.Timesheets.Where(x=>x.TimesheetId==tId && x.UserId == uid).FirstOrDefault();
			if (entry != null)
			{
				_ciPlatformContext.Timesheets.Remove(entry);
				_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
