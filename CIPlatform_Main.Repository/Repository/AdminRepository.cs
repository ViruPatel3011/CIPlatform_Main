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
			var list = _ciPlatformContext.MissionApplications.ToList();
			return list;
		}
		public List<Skill> getSkillsList()
		{
			var list = _ciPlatformContext.Skills.ToList();
			return list;
		}

		public User getDataForUserPanel(long uId)
		{
			var checkUser=_ciPlatformContext.Users.Where(x=>x.UserId == uId).FirstOrDefault();
			return checkUser;
		}

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
				CountryId=1,
				CityId=2

			};
			_ciPlatformContext.Users.Add(user);
			_ciPlatformContext.SaveChanges();
			return true;
		}

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

		public Mission getDataForMissionEdit(long mId)
		{
			var mission = _ciPlatformContext.Missions.Where(x => x.MissionId == mId).FirstOrDefault();
			return mission;
		}

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

		public MissionTheme getDataForMissionThemeEdit(long mthemeId)
		{
			var missionTheme=_ciPlatformContext.MissionThemes.Where(x=>x.MissionThemeId == mthemeId).FirstOrDefault();
			return missionTheme;
		}

		public void EditDataForMissionTheme(AdminViewModel adminView, long missionThemeid)
		{
			var theme = _ciPlatformContext.MissionThemes.Where(x => x.MissionThemeId == missionThemeid).FirstOrDefault();
			theme.Title = adminView.themeTitle;
			theme.CreatedAt = adminView.createdDate;
			theme.Status = adminView.themeStatus;
			_ciPlatformContext.SaveChanges();

		}

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

		public bool SaveSkillsData(string SName, DateTime SDate, string SStatus)
		{
			Skill skill = new Skill() { 
				
				SkillName=SName,
				CreatedAt=SDate,
				Status=SStatus
			
			};
			_ciPlatformContext.Skills.Add(skill);
			_ciPlatformContext.SaveChanges();
			return true;

		}
	}
}
