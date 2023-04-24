using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Interface
{
	public interface IUserRepository
	{
		public UserViewModel getUserData(int uid);

		// Method for Save User Profile Data
		public void SaveUserProfile(UserViewModel userView,int uid);


		// Method for Add User Skill Data
		public int AddSkills(long[] skillsArray, int uid);

		// Method for change user Password
		public int ChangeUserPassword(int uid, UserViewModel userViewModel);

		// Method for change user Profile image
		public string changeAvatar(string image, int uid);


		// Method for Getting List of Country for UserProfile
		public List<Country> GetCountryList();


		// Method for Getting List of City for UserProfile
		public List<City> GetCityList(int id);



		//*******   Timesheet Section START ********
		public void AddTimeSheetData(VolTimeSheetVM volTime, int uid);
		public void AddGoalBaseData(VolTimeSheetVM volTime, int uid);

		// Below method is for Get List of TimeSheet for Time Based mission
		public List<Timesheet> getTimeBasedSheet(int uid);

		// Below method is for Get List of TimeSheet for Goal Based mission
		public List<Timesheet> getGoalBasedSheet(int uid);

		// Below method is for Get List of Time Based mission
		public List<Mission> getTimeBaseMission(int uid);

		// Below method is for Get List of Goal Based mission
		public List<Mission> getGoalBaseMission(int uid);
		


		// Get Data for Edit TimeBased Mission
		public Timesheet getDataForEditSectionForTimeBase(long tId, long uid);

		public Timesheet getDataForEditSectionForGoalBase(long goalBasedId, long uid);


		public string getMissionNameForEditSection(long tId);


		public void editDataForTimeMission(VolTimeSheetVM vtvm, int tId, long uid);
		public void editDataForGoalMission(VolTimeSheetVM vtvm, long timeSheetId, long uid);


		// Delete data from timesheet
		public bool removeTimeBasedData(int tId, int uid);


	}
}
