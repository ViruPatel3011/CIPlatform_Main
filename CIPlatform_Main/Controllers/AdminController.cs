﻿using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml;

namespace CIPlatform_Main.Controllers
{
	[Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{


		private readonly IAdmin _admin;

		public AdminController(IAdmin admin)
		{
			_admin = admin;
		}





		[Authorize]
		// Default User Page
		public IActionResult User()
		{
			var userData=_admin.GetUserList();
			AdminViewModel model = new AdminViewModel()
			{
				UserList = userData,
			};

			return View( model);
		}


		// Method for UserPage
		
		public IActionResult UserPage()
		{
			var userData = _admin.GetUserList();
			AdminViewModel model = new AdminViewModel()
			{
				UserList = userData,
			};

			return PartialView("_UserPartial", model);
		}



		// Method for CMSPage
		
		public IActionResult CMSPage()
		{
			var cmsData = _admin.CmsList();
            var userData = _admin.GetUserList();
            AdminViewModel cmsModel = new AdminViewModel()
			{
				CMSPageList = cmsData,
				UserList=userData
			};
			return PartialView("_CMSPartial", cmsModel);
		}



		// Method for CMS Page Add Section
		
		public IActionResult CMSAdd()
		{
			var cmsData = _admin.CmsList();
            var userData = _admin.GetUserList();
            AdminViewModel cmsModel1 = new AdminViewModel()
			{
				CMSPageList = cmsData,
				UserList=userData
			};
			return PartialView("_CMSPageAddRight", cmsModel1);
		}

		// Method for CMS Page Edit Section
		
		public IActionResult CMSEdit(long loadCMsid)
		{
			//var cmsData = _admin.cmsList();
			//         var userData = _admin.getUserList();
			//         AdminViewModel cmsModel2 = new AdminViewModel()
			//{
			//	CMSPageList = cmsData,
			//	UserList= userData
			//};
			var cmsEdit = _admin.GetSingleCMsData(loadCMsid);
			return PartialView("_CMSPageEditRight",cmsEdit);
		}



		// Method for Mission Page
	
		public IActionResult MissionPage()
		{
			var missions = _admin.GetMissionList();
            var userData = _admin.GetUserList();
            AdminViewModel missionModel = new AdminViewModel()
			{
				MissionList = missions,
				UserList=userData
			};

			return PartialView("_MissionPartial", missionModel);
		}

		// Method for Mission Page Add Section
		public IActionResult MissionAdd()
		{
            var userData = _admin.GetUserList();
            var missionData = _admin.GetMissionList();
			var missionTheme = _admin.GetMissionThemeList();
			var skill = _admin.GetSkillsList();
			AdminViewModel missionModel1 = new AdminViewModel()
			{
				MissionList = missionData,
                MissionThemeList= missionTheme,
                SkillList= skill,
				UserList=userData
            };
			return PartialView("_MissionPageAddRight", missionModel1);
		}

		// Method for CMS Page Edit Section
		public IActionResult MissionEdit(long mId)
		{
			//var singleMission = _admin.singleMissionForEdit(missionId);
			//var missionData = _admin.getMissionList();
			//var missionTheme = _admin.getMissionThemeList();
			//var skill = _admin.getSkillsList();
			//var country = _admin.getCountryList();
			//AdminViewModel missionEditModel = new AdminViewModel()
			//{
			//	MissionList = missionData,
			//	MissionThemeList = missionTheme,
			//	SkillList = skill,
			//	CountryList = country
			//};
			var singleMission=_admin.GetMissionData(mId);
			return PartialView("_MissionPageEditRight", singleMission);
		}

	
		// Method for MissionTheme Page
		public IActionResult MissionThemePage()
		{
            var userData = _admin.GetUserList();
            var missionTheme = _admin.GetMissionThemeList();
			AdminViewModel missionThemeModel = new AdminViewModel()
			{ 
				MissionThemeList= missionTheme,
				UserList=userData
			};

			return PartialView("_MissionThemePartial", missionThemeModel);
		}


		// Method for MissionSkill Page
		public IActionResult MissionSkillsPage()
		{
            var userData = _admin.GetUserList();
            var skillsData = _admin.GetSkillsList();
			AdminViewModel skillModel = new AdminViewModel()
			{
				SkillsList = skillsData,
				UserList=userData
			};
			return PartialView("_MissionSkillsPartial", skillModel);
		}



		// Method for MissionApplication Page
		public IActionResult MissionApplicationPage()
		{
			var missionApp = _admin.GetMissionAppList();
			var mission = _admin.GetMissionList();
			var user = _admin.GetUserList();
			
			AdminViewModel missionAppModel = new AdminViewModel()
			{
				MissionApplicationList = missionApp,
				UserList = user,
				MissionList=mission
			};
			return PartialView("_MissionApplicationPartial", missionAppModel);
		}


		// Method for Story Page
		public IActionResult StoryPage()
		{
			var storyList = _admin.GetStoryList();
			var userList = _admin.GetUserList();
			var missionList = _admin.GetMissionList();
			AdminViewModel storyModel = new AdminViewModel() {

				StoryList = storyList,
				UserList=userList,
				MissionList=missionList
			};

			return PartialView("_StoryPartial", storyModel);
		}


		// Method for Banner Page
		public IActionResult BannerPage()
		{
            var userData = _admin.GetUserList();
            var bannerData = _admin.GetBannerList();
			AdminViewModel bannerModel = new AdminViewModel()
			{
				BannerList = bannerData,
				UserList=userData
			};

			return PartialView("_BannerPartial", bannerModel);
		}
		
		public IActionResult TimeSheet()
		{
            var userData = _admin.GetUserList();
			var timesheetdata = _admin.GetTimeSheetTimeList();
			var missionList = _admin.GetMissionList();
			AdminViewModel timesheetModel = new AdminViewModel()
			{
				TimeSheetListTime = timesheetdata,
				UserList=userData,
				MissionList = missionList
			};

			return PartialView("_TimesheetPartial", timesheetModel);
		}
		public IActionResult GoalTimeSheetAdd()
		{
            var userData = _admin.GetUserList();
			var goaldata = _admin.GetTimeSheetGoalList();
			var missionList = _admin.GetMissionList();
			AdminViewModel timesheetModel = new AdminViewModel()
			{
				TimeSheetListGoal = goaldata,
				UserList=userData,
				MissionList = missionList
			};

			return PartialView("_GoalTimeSheet", timesheetModel);
		}


		//***********     User Page Method Start   **************///

		// Method for Add User data 
		[HttpPost]
		public IActionResult AddUserData(string Ufname, string Ulname, string Uemail,string Upwd,string UphnNumber, string Uavtar, string Uempid, string UDept,  string Usts,int Ucountry,int Ucity)
		{
			var userDataAdded = _admin.AddUserDetails(Ufname,Ulname,Uemail,Upwd,UphnNumber,Uavtar,Uempid,UDept,Usts, Ucountry, Ucity);
			if (userDataAdded)
			{
				TempData["Success Message"] = "User Added Successfully";
				return RedirectToAction("UserPage", "Admin");
			}
			else
			{
				TempData["Error Message"] = "User already Exist!";
				return RedirectToAction("UserPage", "Admin");

			}
			
		}


		// Method for get UserData for Edit
		[HttpGet]
		public IActionResult GetDataForUser(long uId)
		{
           
			var data = _admin.GetDataForUserPanel(uId);
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

		// Method for Edit User data
		[HttpPost]
        public IActionResult EditUserData(string Name,string Surname, string email,string EmployeeId,long userid,string DeptName,string Ustatus)
		{
			var  editData=_admin.EditDataForUser(Name,Surname,email,EmployeeId,userid,DeptName,Ustatus);
			if (editData)
			{

				TempData["Success Message"] = "Data Edited Successfully";
                return Json(new { redirectUrl = Url.Action("UserPage", "Admin") });

            }
			else
			{
                TempData["Error Message"] = "Data Not Edited ";
                return Json(new { redirectUrl = Url.Action("UserPage", "Admin") });
            }
		}
		public IActionResult EditUserPageData(AdminViewModel adminView,long userId)
		{
			var editData = _admin.EditDataForUserPage(adminView, userId);
			if (editData)
			{

				TempData["Success Message"] = "Data Edited Successfully";
				return RedirectToAction("UserPage", "Admin");

			}
			else
			{
				TempData["Error Message"] = "Data Not Edited ";
				return RedirectToAction("UserPage", "Admin");
			}
		}

		// Method for Delete User Data
		public IActionResult DeleteUserData(long uId)
		{
            var removeUserData = _admin.RemoveUserData(uId);
            if (removeUserData)
            {
                TempData["Success Message"] = "User Deleted Successfully";
                return Json(new { redirectUrl = Url.Action("UserPage", "Admin") });
            }
            else
            {
                TempData["Error Message"] = "Can't Able to delete Because User already used SomeWhere";
                return Json(new { redirectUrl = Url.Action("UserPage", "Admin") });
            }
        }

		//**************   User Page Methods END    ***************///


		//**************   CMS Page Methods START    ***************///

		// Method for Add CMS Page Data
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


		// Method for Delete CMS Page Data
		public IActionResult DeleteCMSPageData(long cmsId)
		{
			var removeCMSData = _admin.RemoveCMSData(cmsId);
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
		public IActionResult GetCMSDataForEdit(long cmsid)
		{
			var cmsData = _admin.GetCMSPageDataforEdit(cmsid);
			return Json(new
			{

				cmsTitle = cmsData.Title,
				cmsText = cmsData.Description,
				cmsSlug = cmsData.Slug,
				cmsStatus = cmsData.Status,
				cmsId = cmsid


			}); ;

		}

		[HttpPost]
		public IActionResult EditCMSPageData(AdminViewModel adminView,long cmsPageid)
		{
			_admin.EditCMSPageData(adminView, cmsPageid);
			TempData["Success Message"] = "Data edited successfully";
			return RedirectToAction("CMSPage", "Admin");

		}

		//**************   Mission Page Methods START    ***************///

		// Method for Add Mission Data
		//[HttpPost]
		//public IActionResult SaveMissionData(string mTitle, string mType,DateTime SDate, DateTime EDate,string msts)
		//{
		//	var dataAdded=_admin.SavedMissionData(mTitle,mType,SDate, EDate,msts);
		//	if (dataAdded)
		//	{
		//		TempData["Success Message"] = "Data  Added Successfully";
		//		return RedirectToAction("MissionPage", "Admin");
		//	}
		//	else
		//	{
		//		TempData["Error Message"] = "Data not Added Successfully";
		//		return RedirectToAction("MissionPage", "Admin");
		//	}
		//}

		// Second 
		public IActionResult AddMissionPageData(AdminViewModel missionVM, List<long> listOfSkill)
		{
			var dataAdd = _admin.AddMissionPagedata(missionVM, listOfSkill);
			if (dataAdd)
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

        // Method for Get Data for Edit Mission 
        [HttpGet]
		public IActionResult GetDataForEditMission(long mId)
		{
			var data = _admin.GetDataForMissionEdit(mId);
			var docData = _admin.GetDataforDocEdit(mId);
            var imageUrl = data.MissionMedia.Select(m => m.MediaPath).ToArray();
			var docsurl = docData.MissionDocuments.Select(m => m.DocumentPath).ToArray();
            return Json(new
			{
				mText = data.Title,
				mDesc = data.Description,
				mSdesc = data.ShortDescription,
				country = data.CountryId,
				city = data.CityId,	
				mOrgN = data.OrganizationName,
				mOrgD = data.OrganizationDetail,
				mSDate = data.StartDate,
				mEDate = data.EndDate,
				mType = data.MissionType,
				mTheme = data.ThemeId,
				mAvailability = data.Availability,
                imageUrls = imageUrl,
				documentUrls = docsurl,
                mEditid = mId


			});
		}

		[HttpPost]
		public IActionResult AddEditedData(AdminViewModel adm,List<long> listOfSkill,long mId,List<IFormFile> images,List<IFormFile> Documents)
		{
			var dataEdit = _admin.EditMissionPageDatainDB(adm, listOfSkill, mId,images,Documents);
			if (dataEdit)
			{
				TempData["Success Message"] = "Mission Edited Successfully";
				return RedirectToAction("MissionPage", "Admin");
			}
			else
			{
				TempData["Success Message"] = "Mission Can't Edited Successfully";
				return RedirectToAction("MissionPage", "Admin");
			}
		}

		// Method for Edit Mission Data 

		[HttpPost]
		public IActionResult EditMissionPageData(AdminViewModel missionVM,long mPageEditId)
		{
			_admin.EditMissionPageData(missionVM, mPageEditId);
            TempData["Success Message"] = "Mission Deleted Successfully";
            return Json(new { redirectUrl = Url.Action("MissionPage", "Admin") });

        }


        // Method for Delete Mission Data 
        public IActionResult DeleteMissionData(long missionId)
		{
			var removeMissionData = _admin.RemoveMissionsData(missionId);
			if (removeMissionData)
			{
				TempData["Success Message"] = "Mission Deleted Successfully";
				return Json(new { redirectUrl = Url.Action("MissionPage", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Can't Able to delete Mission";
				return Json(new { redirectUrl = Url.Action("MissionPage", "Admin") });
			}

		}
		//**************   Mission Page Methods END    ***************///



		//**************   MissionTheme Page Methods START    ***************///


		// Method for Add Missiontheme Data
		public IActionResult SaveMissionThemeData(string titleT,DateTime createT,int statusT)
		{
			var missionThemeAdded = _admin.SaveMisionThemeData( titleT, createT, statusT);
			if (missionThemeAdded)
			{
				TempData["Success Message"] = "MissionTheme Added Successfully";
				return Json(new { redirectUrl = Url.Action("MissionThemePage", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "MissionTheme can't Add ";
				return Json(new { redirectUrl = Url.Action("MissionThemePage", "Admin") });
			}
		}



		// Method for Get Data for Missiontheme 
		[HttpGet]
		public IActionResult GetDataForEditMissionTheme(long mthemeId)
		{
			var themedata = _admin.GetDataForMissionThemeEdit(mthemeId);
			return Json(new
			{
				themeTitle = themedata.Title,
				createdtheme = themedata.CreatedAt,
				tStatus = themedata.Status,
				mIdT = mthemeId

			});

		}

		// Method for Edit Missiontheme Data
		[HttpPost]
		public IActionResult EditMissionThemeData(AdminViewModel adminView,long missionThemeid)
		{
			_admin.EditDataForMissionTheme(adminView, missionThemeid);
			TempData["Success Message"] = "Data edited successfully";
			return RedirectToAction("MissionThemePage", "Admin");

		}

		// Method for Delete Missiontheme Data
		public IActionResult DeleteMissionThemeData(long ThId)
		{
			var removeMissionThemeData = _admin.RemoveMissionThemeData(ThId);
			if (removeMissionThemeData)
			{
				TempData["Success Message"] = "MissionTheme Deleted Successfully";
				return Json(new { redirectUrl = Url.Action("MissionThemePage", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Can't Able to delete MissionTheme";
				return Json(new { redirectUrl = Url.Action("MissionThemePage", "Admin") });
			}
		}
		//**************   MissionTheme Page Methods END    ***************///



		//**************   MissionApplication Page Methods START    ***************///

		// Method for Add MissionApplication Data
		public IActionResult AddMissionApplicationData(long mappMid,long mappUid,DateTime mappADate, DateTime mappCDate)
		{
			var missionappAdded = _admin.SaveMisionApplicationData(mappMid, mappUid, mappADate, mappCDate);
			if (missionappAdded)
			{
				TempData["Success Message"] = "MissionApplication Added Successfully";
				return Json(new { redirectUrl = Url.Action("MissionApplicationPage", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "MissionApplication can't Add ";
				return Json(new { redirectUrl = Url.Action("MissionApplicationPage", "Admin") });
			}
		}


		// Method for Approved User by Admin
		[HttpPost]
		public IActionResult AdminApprovedUser(long mAppId,long userId)
		{
			var approved = _admin.ApprovedUserbyAdmin(mAppId, userId);
			if (approved)
			{
				TempData["Success Message"] = "User Approved Successfully";
				return Json(new { redirectUrl = Url.Action("MissionApplicationPage", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "User not approved";
				return Json(new { redirectUrl = Url.Action("MissionApplicationPage", "Admin") });
			}
		}

		// Method for Reject User by Admin
		[HttpPost]
		public IActionResult AdminRejectUser(long missionAppId)
		{
			var rejected = _admin.RejectedUserbyAdmin(missionAppId);
			if (rejected)
			{
				TempData["Success Message"] = "User Rejected Successfully";
				return Json(new { redirectUrl = Url.Action("MissionApplicationPage", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "User not approved";
				return Json(new { redirectUrl = Url.Action("MissionApplicationPage", "Admin") });
			}
		}
		//**************   MissionApplication Page Methods END    ***************///





		//**************   MissionSkill Page Methods START    ***************///

		// Method for Add Skills data
		[HttpPost]
		public IActionResult SaveSkillData(string SName,DateTime SDate /*,string SStatus*/)
		{
			var skillAdd = _admin.SaveSkillsData(SName, SDate /*,SStatus*/);
			if (skillAdd)
			{
				TempData["Success Message"] = "Skill Added Successfully";
				return Json(new { redirectUrl = Url.Action("MissionSkillsPage", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Skill not Added";
				return Json(new { redirectUrl = Url.Action("MissionSkillsPage", "Admin") });
			}
		}

		// Method for Get Data for Missiontheme 
		[HttpGet]
		public IActionResult GetDataForEditMissionSkill(long mSkillid)
		{
			var skilldata = _admin.GetDataForMissionSkillEdit(mSkillid);
			return Json(new
			{

				skillTitleE=skilldata.SkillName,
				skillDateE=skilldata.CreatedAt.ToString("dd-MM-yyyy"),
				mIdS=mSkillid
			});

		}

		[HttpPost]
		public IActionResult EditMissionSkillData(string skillName, long missionSkillid)
		{
			_admin.EditSkillPageData(skillName, missionSkillid);
			TempData["Success Message"] = "Data edited successfully";
			return RedirectToAction("MissionSkillsPage", "Admin");
		}

		// Method for Delete Skills data
		public IActionResult DeleteUserSkill(long SkillsId)
		{
			var deleteSkill = _admin.DeleteSkillByAdmin(SkillsId);
			if (deleteSkill)
			{
				TempData["Success Message"] = "Skill Deleted Successfully";
				return Json(new { redirectUrl = Url.Action("MissionSkillsPage", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Skill not Deleted";
				return Json(new { redirectUrl = Url.Action("MissionSkillsPage", "Admin") });
			}
		}
		//**************   MissionSkill Page Methods END    ***************///





		//**************   Story Page Methods START    ***************///

		public IActionResult AdminApproveStory(long stId)
		{
			_admin.StoryApprovedByAdmin(stId);
			TempData["Success Message"] = "Story Approved";
			return Json(new { redirectUrl = Url.Action("StoryPage", "Admin") });

		}
		
		public IActionResult AdminDeclineStory(long storyid)
		{
			_admin.StoryDeclinedByAdmin(storyid);
			TempData["Success Message"] = "Story Declined";
			return Json(new { redirectUrl = Url.Action("StoryPage", "Admin") });

		}
		public IActionResult AdminDeleteStory(long dSId)
		{
			_admin.StoryDeletedByAdmin(dSId);
			TempData["Success Message"] = "Story Deleted";
			return Json(new { redirectUrl = Url.Action("StoryPage", "Admin") });

		}

		//**************   Story Page Methods END***************///





		//**************   Banner Page Methods START***************///
		public IActionResult AddBannerData(AdminViewModel banner)
		{
			var bannerAdd=_admin.AddbannerPageData(banner);
			if (bannerAdd)
			{

			TempData["Success Message"] = "Banner Added";
			return RedirectToAction("BannerPage", "Admin");
			}
			else
			{
				TempData["Error Message"] = "Sort order already Exist!";
				return RedirectToAction("BannerPage", "Admin");
			}
		}
		

		public IActionResult GetDataForEditBanner(long bId)
			{
			var editbanner = _admin.GetDataForEditBannerPage(bId);
			return Json(new {

				//bImage=editbanner.Image,
				bText=editbanner.Text,
				bOrder=editbanner.SortOrder,
				bDate=editbanner.CreatedAt,
				bannerId=bId

			});

		}

		//get Banner
		public IActionResult GetBanner(long bId)
		{
			var storyMedia = _admin.GetBanner(bId);
			return Json(storyMedia);
		}
		

		public IActionResult EditBannerData(AdminViewModel banner,long bannerId)
		{
			_admin.EditBannerPageData(banner, bannerId);
			TempData["Success Message"] = "Banner Edited";
			return RedirectToAction("BannerPage", "Admin");
		}

		public IActionResult DeleteBannerData(long bannerPageId)
		{
			var delete = _admin.DeleteBannerPageData(bannerPageId);
			if(delete)
			{
				TempData["Success Message"] = "Banner Deleted";
				return RedirectToAction("BannerPage", "Admin");
			}
			else
			{
				TempData["Error Message"] = "Banner not Deleted";
				return RedirectToAction("BannerPage", "Admin");
			}
		}



		// Timesheet Section starts

		// Below method is for Approved TimeBased sheet
		public IActionResult ApprovedTimeSheet(long timeId)
		{
			var timesheetApproved = _admin.ApprovedTimeBasedSheet(timeId);
			if (timesheetApproved)
			{
				TempData["Success Message"] = "Timesheet Approved Successfully";
				return Json(new { redirectUrl = Url.Action("TimeSheet", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Timesheet Not Approved ";
				return Json(new { redirectUrl = Url.Action("TimeSheet", "Admin") });
			}
		}


		// Below method is for Declined TimeBased sheet
		public IActionResult RejectTimeSheet(long rejectTid)
		{
			var rejectTime = _admin.RejectTimeBasedSheet(rejectTid);
			if (rejectTime)
			{
				TempData["Success Message"] = "Timesheet Rejected Successfully";
				return Json(new { redirectUrl = Url.Action("TimeSheet", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Timesheet Not  Rejected ";
				return Json(new { redirectUrl = Url.Action("TimeSheet", "Admin") });
			}
		}
		// Below method is for Delete TimeBased sheet
		public IActionResult DeleteTimeSheet(long deleteTid)
		{
			var deleteTime = _admin.DeleteTimeBasedSheet(deleteTid);
			if (deleteTime)
			{
				TempData["Success Message"] = "Timesheet Deleted Successfully";
				return Json(new { redirectUrl = Url.Action("TimeSheet", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Timesheet Not  Deleted ";
				return Json(new { redirectUrl = Url.Action("TimeSheet", "Admin") });
			}
		}


		// Below method is for Approved GoalBased sheet
		public IActionResult ApprovedTimeSheetGoal(long goalId)
		{
			var timesheetApproved = _admin.ApprovedGoalBasedSheet(goalId);
			if (timesheetApproved)
			{
				TempData["Success Message"] = "Timesheet Approved Successfully";
				return Json(new { redirectUrl = Url.Action("TimeSheet", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Timesheet Not Approved ";
				return Json(new { redirectUrl = Url.Action("TimeSheet", "Admin") });
			}
		}

		// Below method is for Reject GoalBased sheet
		public IActionResult RejectTimeSheetGoal(long rejectgid)
		{
			var rejectGoal = _admin.RejectGoalBasedSheet(rejectgid);
			if (rejectGoal)
			{
				TempData["Success Message"] = "Timesheet Rejected Successfully";
				return Json(new { redirectUrl = Url.Action("TimeSheet", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Timesheet Not  Rejected ";
				return Json(new { redirectUrl = Url.Action("TimeSheet", "Admin") });
			}
		}
	}
}
