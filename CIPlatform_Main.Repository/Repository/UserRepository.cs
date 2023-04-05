using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
				Avatar=loginUser.Avatar
			};
			return userViewModel;
		}

		public void SaveUserProfile(UserViewModel userView, int uid)
		{

			var alreadyExitUser = _ciPlatformContext.Users.Where(x => x.UserId == Convert.ToInt32(uid)).FirstOrDefault();
		

				alreadyExitUser.FirstName = userView.FirstName;
				alreadyExitUser.LastName = userView.LastName;
				alreadyExitUser.EmployeeId = userView.EmployeeId;
				alreadyExitUser.Title = userView.Title;
				alreadyExitUser.Department = userView.Department;
				alreadyExitUser.ProfileText = userView.ProfileText;
				alreadyExitUser.WhyIVolunteer = userView.WhyIVolunteer;
				alreadyExitUser.LinkedInUrl = userView.LinkedInUrl;

				
				_ciPlatformContext.SaveChanges();
				
		


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
	}
}
