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
	}
}
