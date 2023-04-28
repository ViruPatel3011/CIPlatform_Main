using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Xml;

namespace CIPlatform_Main.Controllers
{
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
			var userData=_admin.getUserList();
			AdminViewModel model = new AdminViewModel()
			{
				UserList = userData,
			};

			return View( model);
		}


		// Method for UserPage
		[Authorize(Roles ="Admin")]
		public IActionResult UserPage()
		{
			var userData = _admin.getUserList();
			AdminViewModel model = new AdminViewModel()
			{
				UserList = userData,
			};

			return PartialView("_UserPartial", model);
		}



		// Method for CMSPage
		public IActionResult CMSPage()
		{
			var cmsData = _admin.cmsList();
			AdminViewModel cmsModel = new AdminViewModel()
			{
				CMSPageList = cmsData,
			};
			return PartialView("_CMSPartial", cmsModel);
		}



		// Method for CMS Page Add Section
		public IActionResult CMSAdd()
		{
			var cmsData = _admin.cmsList();
			AdminViewModel cmsModel1 = new AdminViewModel()
			{
				CMSPageList = cmsData,
			};
			return PartialView("_CMSPageAddRight", cmsModel1);
		}

		// Method for CMS Page Edit Section
		public IActionResult CMSEdit()
		{
			var cmsData = _admin.cmsList();
			AdminViewModel cmsModel2 = new AdminViewModel()
			{
				CMSPageList = cmsData,
			};
			return PartialView("_CMSPageEditRight", cmsModel2);
		}



		// Method for Mission Page
		public IActionResult MissionPage()
		{
			var missions = _admin.getMissionList();
			AdminViewModel missionModel = new AdminViewModel()
			{
				MissionList = missions,
			};

			return PartialView("_MissionPartial", missionModel);
		}

		// Method for Mission Page Add Section
		public IActionResult MissionAdd()
		{
			var missionData = _admin.getMissionList();
			var missionTheme = _admin.getMissionThemeList();
			var skill = _admin.getSkillsList();
			AdminViewModel missionModel1 = new AdminViewModel()
			{
				MissionList = missionData,
                MissionThemeList= missionTheme,
                SkillList= skill
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
			var singleMission=_admin.getMissionData(mId);
			return PartialView("_MissionPageEditRight", singleMission);
		}

	
		// Method for MissionTheme Page
		public IActionResult MissionThemePage()
		{

			var missionTheme = _admin.getMissionThemeList();
			AdminViewModel missionThemeModel = new AdminViewModel()
			{ 
				MissionThemeList= missionTheme,
			};

			return PartialView("_MissionThemePartial", missionThemeModel);
		}


		// Method for MissionSkill Page
		public IActionResult MissionSkillsPage()
		{
			var skillsData = _admin.getSkillsList();
			AdminViewModel skillModel = new AdminViewModel()
			{
				SkillsList = skillsData,
			};
			return PartialView("_MissionSkillsPartial", skillModel);
		}



		// Method for MissionApplication Page
		public IActionResult MissionApplicationPage()
		{
			var missionApp = _admin.getMissionAppList();
			var mission = _admin.getMissionList();
			var user = _admin.getUserList();
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
			var storyList = _admin.getStoryList();
			var userList = _admin.getUserList();
			var missionList = _admin.getMissionList();
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
			var bannerData = _admin.getBannerList();
			AdminViewModel bannerModel = new AdminViewModel()
			{
				BannerList = bannerData
			};

			return PartialView("_BannerPartial", bannerModel);
		}


		//***********     User Page Method Start   **************///

		// Method for Add User data 
		[HttpPost]
		public IActionResult addUserData(string Ufname, string Ulname, string Uemail,string Upwd,string UphnNumber, string Uavtar, string Uempid, string UDept,  string Usts)
		{
			var userDataAdded = _admin.AddUserDetails(Ufname,Ulname,Uemail,Upwd,UphnNumber,Uavtar,Uempid,UDept,Usts);
			if (userDataAdded)
			{
				TempData["Success Message"] = "User Added Successfully";
				return RedirectToAction("User", "Admin");
			}
			else
			{
				TempData["Error Message"] = "User already Exist!";
				return RedirectToAction("User", "Admin");

			}
			
		}


		// Method for get UserData for Edit
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

		// Method for Edit User data
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
		public IActionResult EditUserPageData(AdminViewModel adminView,long userId)
		{
			var editData = _admin.EditDataForUserPage(adminView, userId);
			if (editData)
			{

				TempData["Success Message"] = "Data Edited Successfully";
				return RedirectToAction("User", "Admin");

			}
			else
			{
				TempData["Error Message"] = "Data Not Edited ";
				return RedirectToAction("User", "Admin");
			}
		}

		// Method for Delete User Data
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
		public IActionResult getCMSDataForEdit(long cmsid)
		{
			var cmsData = _admin.getCMSPageDataforEdit(cmsid);
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
			return RedirectToAction("User", "Admin");

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
                return RedirectToAction("User", "Admin");
            }
            else
            {
                TempData["Error Message"] = "Data not Added Successfully";
                return RedirectToAction("User", "Admin");
            }
        }

        // Method for Get Data for Edit Mission 
        [HttpGet]
		public IActionResult getDataForEditMission(long mId)
		{
			var data = _admin.getDataForMissionEdit(mId);
			var docData = _admin.getDataforDocEdit(mId);
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
		public IActionResult addEditedData(AdminViewModel adm,List<long> listOfSkill,long mId,List<IFormFile> images,List<IFormFile> Documents)
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
			_admin.editMissionPageData(missionVM, mPageEditId);
            TempData["Success Message"] = "Mission Deleted Successfully";
            return Json(new { redirectUrl = Url.Action("User", "Admin") });

        }


        // Method for Delete Mission Data 
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
		//**************   Mission Page Methods END    ***************///



		//**************   MissionTheme Page Methods START    ***************///


		// Method for Add Missiontheme Data
		public IActionResult SaveMissionThemeData(string titleT,DateTime createT,int statusT)
		{
			var missionThemeAdded = _admin.SaveMisionThemeData( titleT, createT, statusT);
			if (missionThemeAdded)
			{
				TempData["Success Message"] = "MissionTheme Added Successfully";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "MissionTheme can't Add ";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
		}



		// Method for Get Data for Missiontheme 
		[HttpGet]
		public IActionResult getDataForEditMissionTheme(long mthemeId)
		{
			var themedata = _admin.getDataForMissionThemeEdit(mthemeId);
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
			return RedirectToAction("User", "Admin");

		}

		// Method for Delete Missiontheme Data
		public IActionResult deleteMissionThemeData(long ThId)
		{
			var removeMissionThemeData = _admin.removeMissionThemeData(ThId);
			if (removeMissionThemeData)
			{
				TempData["Success Message"] = "MissionTheme Deleted Successfully";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Can't Able to delete MissionTheme";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
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
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "MissionApplication can't Add ";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
		}


		// Method for Approved User by Admin
		[HttpPost]
		public IActionResult AdminApprovedUser(long mAppId)
		{
			var approved = _admin.ApprovedUserbyAdmin(mAppId);
			if (approved)
			{
				TempData["Success Message"] = "User Approved Successfully";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "User not approved";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
		}

		// Method for Reject User by Admin
		[HttpPost]
		public IActionResult AdminRejectUser(long missionAppId)
		{
			var rejected = _admin.RejectedUserbyAdmin(missionAppId);
			if (rejected)
			{
				TempData["Success Message"] = "User Approved Successfully";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "User not approved";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
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
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Skill not Added";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
		}

		// Method for Get Data for Missiontheme 
		[HttpGet]
		public IActionResult getDataForEditMissionSkill(long mSkillid)
		{
			var skilldata = _admin.getDataForMissionSkillEdit(mSkillid);
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
			return RedirectToAction("User", "Admin");
		}

		// Method for Delete Skills data
		public IActionResult deleteUserSkill(long SkillsId)
		{
			var deleteSkill = _admin.DeleteSkillByAdmin(SkillsId);
			if (deleteSkill)
			{
				TempData["Success Message"] = "Skill Deleted Successfully";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
			else
			{
				TempData["Error Message"] = "Skill not Deleted";
				return Json(new { redirectUrl = Url.Action("User", "Admin") });
			}
		}
		//**************   MissionSkill Page Methods END    ***************///





		//**************   Story Page Methods START    ***************///

		public IActionResult AdminApproveStory(long stId)
		{
			_admin.StoryApprovedByAdmin(stId);
			TempData["Success Message"] = "Story Approved";
			return Json(new { redirectUrl = Url.Action("User", "Admin") });

		}
		
		public IActionResult AdminDeclineStory(long storyid)
		{
			_admin.StoryDeclinedByAdmin(storyid);
			TempData["Success Message"] = "Story Declined";
			return Json(new { redirectUrl = Url.Action("User", "Admin") });

		}
		public IActionResult AdminDeleteStory(long dSId)
		{
			_admin.StoryDeletedByAdmin(dSId);
			TempData["Success Message"] = "Story Deleted";
			return Json(new { redirectUrl = Url.Action("User", "Admin") });

		}

		//**************   Story Page Methods END***************///





		//**************   Banner Page Methods START***************///
		public IActionResult AddBannerData(AdminViewModel banner)
		{
			var bannerAdd=_admin.AddbannerPageData(banner);
			if (bannerAdd)
			{

			TempData["Success Message"] = "Banner Added";
			return RedirectToAction("User", "Admin");
			}
			else
			{
				TempData["Error Message"] = "Sort order already Exist!";
				return RedirectToAction("User", "Admin");
			}
		}
		

		public IActionResult getDataForEditBanner(long bId)
			{
			var editbanner = _admin.getDataForEditBannerPage(bId);
			return Json(new {

				//bImage=editbanner.Image,
				bText=editbanner.Text,
				bOrder=editbanner.SortOrder,
				bDate=editbanner.CreatedAt,
				bannerId=bId

			});

		}

		//get Banner
		public IActionResult getBanner(long bId)
		{
			var storyMedia = _admin.getBanner(bId);
			return Json(storyMedia);
		}
		

		public IActionResult EditBannerData(AdminViewModel banner,long bannerId)
		{
			_admin.EditBannerPageData(banner, bannerId);
			TempData["Success Message"] = "Banner Edited";
			return RedirectToAction("User", "Admin");
		}

		public IActionResult deleteBannerData(long bannerPageId)
		{
			var delete = _admin.deleteBannerPageData(bannerPageId);
			if(delete)
			{
				TempData["Success Message"] = "Banner Deleted";
				return RedirectToAction("User", "Admin");
			}
			else
			{
				TempData["Error Message"] = "Banner not Deleted";
				return RedirectToAction("User", "Admin");
			}
		}
	}
}
