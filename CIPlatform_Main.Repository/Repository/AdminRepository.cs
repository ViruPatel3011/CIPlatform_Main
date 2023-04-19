using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using System.Diagnostics.Contracts;

namespace CIPlatform_Main.Repository.Repository
{
	public class AdminRepository:IAdmin
	{
		private readonly CiPlatformContext _ciPlatformContext;

		public AdminRepository(CiPlatformContext ciPlatformContext)
		{
			_ciPlatformContext = ciPlatformContext;
		}


		// All below Method for getting List

		// ************** List Section start    **********//
		public List<User> getUserList()
		{
			var list = _ciPlatformContext.Users.OrderBy(x=>x.Status).ToList();
			return list;
		}
		public List<CmsPage> cmsList()
		{
			var list = _ciPlatformContext.CmsPages.OrderBy(x=>x.Status).ToList();
			return list;
		}	
		public List<Mission> getMissionList()
		{
			var list = _ciPlatformContext.Missions.OrderBy(x=>x.Status).ToList();
			return list;
		}

		public List<MissionTheme> getMissionThemeList()
		{
			var list = _ciPlatformContext.MissionThemes.ToList();
			return list;
		}
		public List<MissionApplication> getMissionAppList()
		{
			var list = _ciPlatformContext.MissionApplications.OrderBy(x=>x.ApprovalStatus).ToList();
			return list;
		}
		public List<Skill> getSkillsList()
		{
			var list = _ciPlatformContext.Skills.ToList();
			return list;
		}
		public List<Story> getStoryList()
		{
			var list = _ciPlatformContext.Stories.ToList();
			return list;
		}
		public List<Banner> getBannerList()
		{
			var list = _ciPlatformContext.Banners.ToList();
			return list;
		}


		// ************** List Section ENd    **********//



		//**************   User Page Methods START    ***************///

		// Method for Add User data
		public bool AddUserDetails(string Ufname, string Ulname, string Uemail, string Upwd, string UphnNumber, string Uavtar, string Uempid, string UDept, string Usts)
		{
			User user = new User()
			{
				FirstName = Ufname,
				LastName = Ulname,
				Email = Uemail,
				Password = Upwd,
				PhoneNumber = UphnNumber,
				Avatar = Uavtar,
				EmployeeId = Uempid,
				Department = UDept,
				Status = Usts,
				CountryId = 1,
				CityId = 2

			};
			_ciPlatformContext.Users.Add(user);
			_ciPlatformContext.SaveChanges();
			return true;
		}

		// Method for get UserData for Edit
		public User getDataForUserPanel(long uId)
		{
			var checkUser=_ciPlatformContext.Users.Where(x=>x.UserId == uId).FirstOrDefault();
			return checkUser;
		}


		// Method for Edit User data
		public bool EditDataForUser(string Name, string Surname, string email, string EmployeeId, long userid, string DeptName, string Ustatus)
		{
			var isUserValid=_ciPlatformContext.Users.Where(x=>x.UserId==userid).FirstOrDefault();
			isUserValid.FirstName = Name;
			isUserValid.LastName = Surname;
			isUserValid.Email = email;
			isUserValid.EmployeeId = EmployeeId;
			isUserValid.Department = DeptName;
			isUserValid.Status = Ustatus;
			_ciPlatformContext.SaveChanges();
			return true;
		}


		// Method for Remove User data
		public bool removeUserData(long uId)
		{
			var isValidUser=_ciPlatformContext.Users.Where(x=>x.UserId==uId).FirstOrDefault();
			isValidUser.Status = "Deactive";
			isValidUser.DeletedAt = DateTime.Now;
			var userAlreadyInUse = _ciPlatformContext.MissionApplications.Where(x => x.UserId == isValidUser.UserId).FirstOrDefault();
			
			if(userAlreadyInUse == null )
			{
				
				_ciPlatformContext.Users.Update(isValidUser); 
				_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;

            }
		}

		//**************   User Page Methods END    ***************///





		//**************   CMs Page Methods start    ***************///

		// Method for Add CMS Page Data
		public bool AddCMSpageData(string Title, string Description, string Slug, string Status)
		{
			CmsPage cmsPage = new CmsPage()
			{
				Title = Title,
				Description = Description,
				Slug = Slug,
				Status = Status
			};
			_ciPlatformContext.CmsPages.Add(cmsPage);
			_ciPlatformContext.SaveChanges();
			return true;
		}

		public CmsPage getCMSDataForEdit(long cmsid)
		{
			var cmspage = _ciPlatformContext.CmsPages.Where(x => x.CmsPageId == cmsid).FirstOrDefault();
			return cmspage;
		}


		// Method for Delete CMS Page Data
		public bool removeCMSData(long cmsId)
		{
			var cmsDataExist=_ciPlatformContext.CmsPages.Where(x=>x.CmsPageId == cmsId).FirstOrDefault();	
			if(cmsDataExist !=null)
			{
				cmsDataExist.DeletedAt = DateTime.Now;
				cmsDataExist.Status = "Deactive";
				_ciPlatformContext.CmsPages.Update(cmsDataExist);
				_ciPlatformContext.SaveChanges();
				return true;

			}
			else
			{
				return false;
			}
		}

		//**************   CMS Page MethodS END    ***************///






		//**************   Mission Page MethodS START    ***************///

		// Method for Add Mission Data
		public bool SavedMissionData(string mTitle, string mType, DateTime SDate, DateTime EDate, string msts)
		{
			Mission mission = new Mission()
			{
				Title = mTitle,
				MissionType = mType,
				StartDate = SDate,
				EndDate = EDate,
				Status=msts,
				CityId=2,
				CountryId=1,
				ThemeId=1 // by default we should take theme_id as some value otherwise it will throw ans error		
				
			};
			_ciPlatformContext.Missions.Add(mission);
			_ciPlatformContext.SaveChanges();
			return true;
		}

		// Method for Get Data for Edit Mission 
		public Mission getDataForMissionEdit(long mId)
		{
			var mission = _ciPlatformContext.Missions.Where(x => x.MissionId == mId).FirstOrDefault();
			return mission;
		}

		// Method for Edit Mission Data 
		public void editMissionData(AdminViewModel adminView, long missionid)
		{
			var mission = _ciPlatformContext.Missions.Where(x => x.MissionId == missionid).FirstOrDefault();
			mission.Title = adminView.title;
			mission.MissionType = adminView.missionType;
			mission.StartDate = adminView.startDate;
			mission.EndDate= adminView.endDate;
			mission.Status = adminView.status;
			_ciPlatformContext.SaveChanges();
		}

		// Method for Delete Mission Data 
		public bool removeMissionsData(long missionId)
		{
			var validMission = _ciPlatformContext.Missions.Where(x => x.MissionId == missionId).FirstOrDefault();
			if(validMission != null)
			{

			validMission.Status = "Deactive";
			validMission.DeletedAt = DateTime.Now;
			_ciPlatformContext.Missions.Update(validMission);
			_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}


		}
		//**************   Mission Page Methods END    ***************///





		//**************   MissionTheme Page Methods END    ***************///


		// Method for Add Missiontheme Data
		public bool SaveMisionThemeData( string titleT, DateTime createT, int statusT)
		{
			MissionTheme missionTheme = new MissionTheme()
			{
				//MissionThemeId = IdT,
				Title = titleT,
				CreatedAt = createT,
				Status =(byte) statusT
			};
			_ciPlatformContext.MissionThemes.Add(missionTheme);
			_ciPlatformContext.SaveChanges();
			return true;
		}

		// Method for Get Data for Missiontheme 
		public MissionTheme getDataForMissionThemeEdit(long mthemeId)
		{
			var missionTheme=_ciPlatformContext.MissionThemes.Where(x=>x.MissionThemeId == mthemeId).FirstOrDefault();
			return missionTheme;
		}

		// Method for Edit Missiontheme Data
		public void EditDataForMissionTheme(AdminViewModel adminView, long missionThemeid)
		{
			var theme = _ciPlatformContext.MissionThemes.Where(x => x.MissionThemeId == missionThemeid).FirstOrDefault();
			theme.Title = adminView.themeTitle;
			theme.CreatedAt = adminView.createdDate;
			theme.Status = adminView.themeStatus;
			_ciPlatformContext.SaveChanges();

		}

		// Method for Delete Missiontheme Data
		public bool removeMissionThemeData(long ThId)
		{
			var themeId = _ciPlatformContext.MissionThemes.Where(x => x.MissionThemeId == ThId).FirstOrDefault();
			if (themeId != null)
			{
				themeId.Status = 0;
				themeId.DeletedAt = DateTime.Now;
				_ciPlatformContext.MissionThemes.Update(themeId);
				_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}
		//**************   MissionTheme Page Methods END    ***************///






		//**************   MissionApplication Page Methods START    ***************///

		// Method for Add MissionApplication Data
		public bool SaveMisionApplicationData(long mappMid, long mappUid, string mappstatus, DateTime mappADate, DateTime mappCDate)
		{
			MissionApplication missionapp = new MissionApplication()
			{
				MissionId=mappMid,
				UserId=mappUid,
				ApprovalStatus=mappstatus,
				AppliedAt=mappADate,
				CreatedAt=mappCDate

			};
			_ciPlatformContext.MissionApplications.Add(missionapp);
			_ciPlatformContext.SaveChanges();
			return true;
		}

		// Method for Approved User by Admin
		public bool ApprovedUserbyAdmin(long mAppId)
		{
			var missionAppId = _ciPlatformContext.MissionApplications.Where(x => x.MissionApplicationId == mAppId).FirstOrDefault();
			if (missionAppId != null)
			{
				missionAppId.ApprovalStatus = "Approved";
				_ciPlatformContext.MissionApplications.Update(missionAppId);
				_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		// Method for Reject User by Admin
		public bool RejectedUserbyAdmin(long missionAppId)
		{
			var misAppId = _ciPlatformContext.MissionApplications.Where(x => x.MissionApplicationId == missionAppId).FirstOrDefault();
			if (misAppId != null)
			{
				misAppId.ApprovalStatus = "Rejected";
				_ciPlatformContext.MissionApplications.Update(misAppId);
				_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}

		//**************   MissionApplication Page Methods END    ***************///





		//**************   MissionSkill Page Methods START    ***************///

		// Method for Add Skills data
		public bool SaveSkillsData(string SName, DateTime SDate /*, string SStatus*/)
		{
			Skill skill = new Skill() { 
				
				SkillName=SName,
				CreatedAt=SDate,
				//Status=SStatus
				Status = "Added"

			};
			_ciPlatformContext.Skills.Add(skill);
			_ciPlatformContext.SaveChanges();
			return true;

		}

		// Method for Delete Skills data
		public bool DeleteSkillByAdmin(long SkillsId)
		{
			var skill = _ciPlatformContext.Skills.Where(x => x.SkillId == SkillsId).FirstOrDefault();
			if (skill != null)
			{
				skill.Status = "Deleted";
				_ciPlatformContext.Skills.Update(skill);
				_ciPlatformContext.SaveChanges();
				return true;

			}
			else
			{
				return false;
			}
		}
		//**************   MissionSkill Page Methods END    ***************///








		//**************   Story Page Methods START    ***************///

		public bool StoryApprovedByAdmin(long stId)
		{
			var storyid=_ciPlatformContext.Stories.Where(x=>x.StoryId==stId).FirstOrDefault();
			if(storyid != null)
			{
				storyid.Status = "Published";
				_ciPlatformContext.Stories.Update(storyid);
				_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		
		}

		public bool StoryDeclinedByAdmin(long storyid)
		{
			var storyId = _ciPlatformContext.Stories.Where(x => x.StoryId == storyid).FirstOrDefault();
			if (storyId != null)
			{
				storyId.Status = "Declined";
				_ciPlatformContext.Stories.Update(storyId);
				_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public bool StoryDeletedByAdmin(long dSId)
		{
			var storyId = _ciPlatformContext.Stories.Where(x => x.StoryId == dSId).FirstOrDefault();
			if (storyId != null)
			{
				storyId.Status = "Deleted";
				_ciPlatformContext.Stories.Update(storyId);
				_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}
		//**************   Story Page Methods END***************///




		//**************   Banner Page Methods START***************///
		public bool AddBanneData(string textB, string imageB, int sOrderB, DateTime dateB)
		{
			Banner banner = new Banner()
			{ 
				Text=textB,
				Image=imageB,
				SortOrder=sOrderB,
				CreatedAt=dateB
			
			};
			_ciPlatformContext.Banners.Add(banner); 
			_ciPlatformContext.SaveChanges();
			return true;

		}

		public Banner getDataForEditBannerPage(long bId)
		{
			var bannerList = _ciPlatformContext.Banners.Where(x => x.BannerId == bId).FirstOrDefault();
			return bannerList;
		}

		public void EditBannerPageData(BannerVM banner, long bannerId)
		{
			var bannerPageId = _ciPlatformContext.Banners.Where(x => x.BannerId == bannerId).FirstOrDefault();
			bannerPageId.Text = banner.Text;
			bannerPageId.Image = banner.Image;
			bannerPageId.SortOrder = banner.SortOrder;
			bannerPageId.CreatedAt = banner.CreatedAt;
			_ciPlatformContext.Banners.Update(bannerPageId);
			_ciPlatformContext.SaveChanges();
		}

		public bool deleteBannerPageData(long bannerPageId)
		{
			var bid=_ciPlatformContext.Banners.Where(x=>x.BannerId==bannerPageId).FirstOrDefault();
			if(bid!=null) {

				bid.DeletedAt = DateTime.Now;
				_ciPlatformContext.Banners.Update(bid);
				_ciPlatformContext.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
