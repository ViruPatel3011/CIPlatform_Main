using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CIPlatform_Main.Controllers
{
	public class AdminController : Controller
	{


		private readonly IAdmin _admin;

		public AdminController(IAdmin admin)
		{
			_admin = admin;
		}





		public IActionResult Index()
		{
			return View();
		}
		
		
		public IActionResult User()
		{
			var userData=_admin.getUserList();
			AdminViewModel model = new AdminViewModel()
			{
				UserList = userData,
			};

			return View( model);
		}

		public IActionResult UserPage()
		{
			var userData = _admin.getUserList();
			AdminViewModel model = new AdminViewModel()
			{
				UserList = userData,
			};

			return PartialView("_UserPartial", model);
		}
		
		public IActionResult CMSPage()
		{
			var cmsData = _admin.cmsList();
			AdminViewModel cmsModel = new AdminViewModel()
			{
				CMSPageList = cmsData,
			};
			return PartialView("_CMSPartial", cmsModel);
		}
		
		public IActionResult CMSAdd()
		{
			return PartialView("_CMSAddPartial");
		}
		
		public IActionResult CMSEdit()
		{
			return PartialView("_CMSEditPartial");
		}
		public IActionResult MissionPage()
		{
			var missions = _admin.getMissionList();
			AdminViewModel missionModel = new AdminViewModel()
			{
				MissionList = missions,
			};

			return PartialView("_MissionPartial", missionModel);
		}
		public IActionResult MissionThemePage()
		{

			var missionTheme = _admin.getMissionThemeList();
			AdminViewModel missionThemeModel = new AdminViewModel()
			{ 
				MissionThemeList= missionTheme,
			};

			return PartialView("_MissionThemePartial", missionThemeModel);
		}
		
		public IActionResult MissionSkillsPage()
		{
			return PartialView("_MissionSkillsPartial");
		}
		public IActionResult MissionApplicationPage()
		{
			return PartialView("_MissionApplicationPartial");
		}
		
		public IActionResult StoryPage()
		{
			return PartialView("_StoryPartial");
		}
		public IActionResult BannerPage()
		{
			return PartialView("_BannerPartial");
		}

		[HttpGet]
		public IActionResult getDataForUser(long uId)
		{
           
			var data = _admin.getDataForUserPanel(uId);
            return Json(new
            {
                firstName = data.FirstName,
                lastName = data.LastName,
                email = data.Email,
                employeeId = data.EmployeeId,
                department = data.Department,
                status = data.Status,
                userId = uId

            });
        }


		[HttpPost]
        public IActionResult EditUserData(string Name,string Surname, string email,string EmployeeId,long userid,string DeptName,string Ustatus)
		{
			var  editData=_admin.EditDataForUser(Name,Surname,email,EmployeeId,userid,DeptName,Ustatus);
			if (editData)
			{

				TempData["Success Message"] = "Data Edited Successfully";
                return Json(new { redirectUrl = Url.Action("User", "Admin") });

            }
			else
			{
                TempData["Error Message"] = "Data Not Edited ";
                return Json(new { redirectUrl = Url.Action("User", "Admin") });
            }
		}

		public IActionResult deleteUserData(long uId)
		{
            var removeUserData = _admin.removeUserData(uId);
            if (removeUserData)
            {
                TempData["Success Message"] = "User Deleted Successfully";
                return Json(new { redirectUrl = Url.Action("User", "Admin") });
            }
            else
            {
                TempData["Error Message"] = "Can't Able to delete Because User already used SomeWhere";
                return Json(new { redirectUrl = Url.Action("User", "Admin") });
            }
        }

		[HttpPost]
		public IActionResult addUserData(string Ufname, string Ulname, string Uemail,string Upwd,string UphnNumber, string Uavtar, string Uempid, string UDept,  string Usts)
		{
			var userDataAdded = _admin.AddUserDetails(Ufname,Ulname,Uemail,Upwd,UphnNumber,Uavtar,Uempid,UDept,Usts);
			if (userDataAdded)
			{
				TempData["Success Message"] = "Data Added Successfully";
				return RedirectToAction("User", "Admin");
			}
			else
			{
				TempData["Error Message"] = "Data not Added Successfully";
				return RedirectToAction("User", "Admin");

			}
			
		}

		[HttpPost]

		public IActionResult AddCMSPageData(string Title, string Description, string Slug, string Status)
		{
			var cmsPageDataAdd = _admin.AddCMSpageData(Title, Description, Slug, Status);
			if (cmsPageDataAdd)
			{
				TempData["Success Message"] = "Data Added Successfully";
				return RedirectToAction("CMSPage", "Admin");
			}
			else
			{
				TempData["Error Message"] = "Data not Added Successfully";
				return RedirectToAction("CMSPage", "Admin");
			}
		}

		//[HttpGet]
		//public IActionResult getCMSDataForEdit(long cmsid)
		//{
		//	var cmsData = _admin.getCMSPageDataforEdit(cmsid);
		//	return Json(new {
				

				
		//	});

		//}


		[HttpPost]
		public IActionResult SaveMissionData(string mTitle, string mType,DateTime SDate, DateTime EDate)
		{
			var dataAdded=_admin.SavedMissionData(mTitle,mType,SDate, EDate);
			if (dataAdded)
			{
				TempData["Success Message"] = "Data  Added Successfully";
				return RedirectToAction("MissionPage", "Admin");
			}
			else
			{
				TempData["Error Message"] = "Data not Added Successfully";
				return RedirectToAction("MissionPage", "Admin");
			}
		}


		public IActionResult deleteCMSPageData(long cmsId)
		{
			var removeCMSData = _admin.removeCMSData(cmsId);
			if (removeCMSData)
			{
				TempData["Success Message"] = "CMS Data Deleted Successfully";
				return Json(new { redirectUrl = Url.Action("CMSPage", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Can't Able to delete CMS Data";
				return Json(new { redirectUrl = Url.Action("CMSPage", "Admin") });
			}
		}

		[HttpGet]
		public IActionResult getDataForEditMission(long mId)
		{
			var data = _admin.getDataForMissionEdit(mId);
			return Json(new
			{
				mTitle=data.Title,
				mType=data.MissionType,
				sDate=data.StartDate,
				eDate=data.EndDate,
				mStatus=data.Status,
				mid=mId

			});
		}

		[HttpPost]
		public IActionResult EditMissionData(AdminViewModel adminView, long missionid)
		{
			_admin.editMissionData(adminView, missionid);
			TempData["Success Message"] = "Data edited successfully";
			return RedirectToAction("User", "Admin");
		}

		public IActionResult deleteMissionData(long missionId)
		{
			var removeMissionData = _admin.removeMissionsData(missionId);
			if (removeMissionData)
			{
				TempData["Success Message"] = "Mission Deleted Successfully";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Can't Able to delete Mission";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}

		}

	}
}
