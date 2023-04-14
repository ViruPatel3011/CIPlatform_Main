﻿using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;

using Microsoft.AspNetCore.Mvc;
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
			return PartialView("_CMSPartial");
		}
		public IActionResult MissionPage()
		{
			return PartialView("_MissionPartial");
		}
		public IActionResult MissionThemePage()
		{
			return PartialView("_MissionThemePartial");
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

	}
}
