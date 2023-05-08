using CIPlatform_Main.Entities.Models;
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
			var userData = _userRepository.GetUserData(Convert.ToInt32(uid));
			
			return View(userData);
		
		}


		// Method for Save User Profile Data
		[HttpPost]
		public IActionResult SaveUserData(UserViewModel userView, long[] finalSkillList) {

			long[] skillsArray = finalSkillList;


			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			_userRepository.SaveUserProfile(userView,Convert.ToInt32(uid));
			_userRepository.AddSkills(skillsArray, Convert.ToInt32(uid));
			TempData["Success Message"] = "Data Added Successfully";
			return RedirectToAction("UserProfile", "User");

		}


		// Method for change user Password
		[HttpPost]
		public IActionResult ChangePassword(UserViewModel userView)
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


		// Method for change user Profile image
		public bool ChangeProfile(string image)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var avtar= _userRepository.ChangeUserAvatar(image, Convert.ToInt32(uid));
			TempData["Success Message"] = "Profile Updated Successfully";
			return true;
		}


		// Method for Getting List of Country for UserProfile
		public JsonResult GetCountryList()
		{
			var countryList = _userRepository.GetCountryListData();
			return Json(countryList);
		}

		// Method for Getting List of City for UserProfile
		public JsonResult GetCityList(int id)
		{
			var cityList = _userRepository.GetCityListData(id);
			return Json(cityList);
		}



		//*******   Timesheet Section START ********
		public IActionResult VolTimeSheet()
		{

			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

			VolTimeSheetVM volTime = new();
			volTime.TimeBasedSheet = _userRepository.GetTimeBasedSheet(Convert.ToInt32(uid));
			volTime.GoalBasedSheet = _userRepository.GetGoalBasedSheet(Convert.ToInt32(uid));
			volTime.MissionListTime = _userRepository.GetTimeBaseMission(Convert.ToInt32(uid));
			volTime.MissionListGoal = _userRepository.GetGoalBaseMission(Convert.ToInt32(uid));
			
			return View(volTime);

		}

		// Add Time based Data
		public IActionResult StoreTimeSheetData(VolTimeSheetVM volTime)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			 _userRepository.AddTimeSheetData(volTime, Convert.ToInt32(uid));
			
				TempData["Success Message"] = "Data Added Successfully";
				return RedirectToAction("VolTimeSheet", "User");
			



		}

		// Add Goal based Data
		public IActionResult StoreGoalData(VolTimeSheetVM volTime)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			_userRepository.AddGoalBaseData(volTime, Convert.ToInt32(uid));
			
				TempData["Success Message"] = "Data Added Successfully";
				return RedirectToAction("VolTimeSheet", "User");
		
				



		}




		//Method for get data for edit in time base

		[HttpGet]
		public IActionResult GetDataForEditSectionForTimeBase(long tId)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var data = _userRepository.GetDataForEditForTimeBase(tId, Convert.ToInt32(uid));
			var dataForMissionName = _userRepository.GetMissionNameForEditSection(tId);
			return Json(new
			{
				missionName = dataForMissionName,
				dateOfVolunteer = data.CreatedAt,
				hours = data.TimesheetTime,
				minutes = data.TimesheetTime,
				message = data.Notes,
				timeId = tId,
			});
		}


		//Method for get data for edit in Goal base
		[HttpGet]
		public IActionResult GetDataForEditSectionForGoalBase(long goalBasedId)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var data = _userRepository.GetDataForEditForGoalBase(goalBasedId, Convert.ToInt32(uid));
			var dataForMissionName = _userRepository.GetMissionNameForEditSection(goalBasedId);

			return Json(new
			{
				missionName = dataForMissionName,
				action = data.Action,
				dateOfVolunteer = data.CreatedAt,
				message = data.Notes,
				goalId=goalBasedId

			});
		}


		// Edit data on Edit button for Time Mission
		[HttpPost]
		public IActionResult EditDataForTimeBaseTimesheet(VolTimeSheetVM vtvm, int timeSheetId)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			_userRepository.EditDataForTimeMission(vtvm, timeSheetId, Convert.ToInt32(uid));
			TempData["Success Message"] = "data edited successfully";
			return RedirectToAction("VolTimeSheet", "User");
		}


		// Edit data on Edit button for Goal Mission
		[HttpPost]
		public IActionResult EditDataForGoalBaseTimesheet(VolTimeSheetVM vtvm, long timeSheetId)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

			_userRepository.EditDataForGoalMission(vtvm, timeSheetId, Convert.ToInt32(uid));
			TempData["Success Message"] = "Data edited successfully";
			return RedirectToAction("VolTimeSheet", "User");
		}


		// Delete method for TimeSheet

		public IActionResult DeleteTimeSheetData(int tId)
		
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var removeData=_userRepository.RemoveTimeBasedData(tId, Convert.ToInt32(uid));
			if (removeData)
			{
				TempData["Success Message"] = "Data Deleted Successfully";
				return RedirectToAction("VolTimeSheet", "User");
			}
			else
			{
				TempData["Error Message"] = "Data Not Deleted ";
				return RedirectToAction("VolTimeSheet", "User");
			}

		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
