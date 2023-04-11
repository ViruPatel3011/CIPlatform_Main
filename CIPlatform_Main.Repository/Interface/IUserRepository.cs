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

		public void SaveUserProfile(UserViewModel userView,int uid);

		public int AddSkills(long[] skillsArray, int uid);

		public int ChangeUserPassword(int uid, UserViewModel userViewModel);

		public string changeAvatar(string image, int uid);

		public void AddTimeSheetData(VolTimeSheetVM volTime, int uid);
		public void AddGoalBaseData(VolTimeSheetVM volTime, int uid);

		public List<Timesheet> getTimeBasedSheet(int uid);
		public List<Timesheet> getGoalBasedSheet(int uid);
		public List<Mission> getTimeBaseMission(int uid);
		public List<Mission> getGoalBaseMission(int uid);
		public List<Country> GetCountryList();
		public List<City> GetCityList(int id);


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
