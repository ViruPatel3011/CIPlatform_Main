﻿using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CIPlatform_Main.Controllers
{
	public class UserController : Controller
	{


		private readonly IUserRepository _userRepository;

		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

	
		public IActionResult UserProfile()
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var userData = _userRepository.getUserData(Convert.ToInt32(uid));
			
			return View(userData);
		
		}

		[HttpPost]
		public IActionResult SaveUserData(UserViewModel userView) {
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			_userRepository.SaveUserProfile(userView,Convert.ToInt32(uid));
			TempData["Success Message"] = "Data Added Successfully";
			return RedirectToAction("UserProfile", "User");

		}

		[HttpPost]
		public IActionResult changePassword(UserViewModel userView)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var userPasswordChanged=_userRepository.ChangeUserPassword(Convert.ToInt32(uid), userView);
			if (userPasswordChanged==1)
			{
				TempData["Success Message"] = "Password Changed Successfully";
				return RedirectToAction("UserProfile");

			}else if(userPasswordChanged==0){
				TempData["Error Message"] = "Old and New Password can't be Same...";
				return RedirectToAction("UserProfile");
			}
			else
			{
				TempData["Error Message"] = "Old Password may be wrong..";
				return RedirectToAction("UserProfile");
			}

		}

		public bool changeProfile(string image)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var avtar= _userRepository.changeAvatar(image, Convert.ToInt32(uid));
			TempData["Success Message"] = "Profile Updated Successfully";
			return true;
		}

		public JsonResult getCountryList()
		{
			var countryList = _userRepository.GetCountryList();
			return Json(countryList);
		}
		public JsonResult getCityList(int id)
		{
			var cityList = _userRepository.GetCityList(id);
			return Json(cityList);
		}

		public IActionResult VolTimeSheet()
		{

			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

			VolTimeSheetVM volTime = new();
			volTime.TimeBasedSheet = _userRepository.getTimeBasedSheet(Convert.ToInt32(uid));
			volTime.GoalBasedSheet = _userRepository.getGoalBasedSheet(Convert.ToInt32(uid));
			volTime.MissionListTime = _userRepository.getTimeBaseMission(Convert.ToInt32(uid));
			volTime.MissionListGoal = _userRepository.getGoalBaseMission(Convert.ToInt32(uid));
			
			return View(volTime);

		}

		public IActionResult storeTimeSheetData(VolTimeSheetVM volTime)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			_userRepository.AddTimeSheetData(volTime, Convert.ToInt32(uid));
			return RedirectToAction("VolTimeSheet", "User");



		}
		public IActionResult storeGoalData(VolTimeSheetVM volTime)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			_userRepository.AddGoalBaseData(volTime, Convert.ToInt32(uid));
			return RedirectToAction("VolTimeSheet", "User");



		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
