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

		public void ChangeUserPassword(int uid, UserViewModel userViewModel);

		public string changeAvatar(string image, int uid);
		public List<Country> GetCountryList();
		public List<City> GetCityList(int id);
	}
}
