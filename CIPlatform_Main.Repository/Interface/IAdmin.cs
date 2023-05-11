using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Interface
{
	public interface IAdmin
	{

		// All below Method for getting List

		// ************** List Section start    **********//
		public List<User> GetUserList();
		public List<CmsPage> CmsList();
		public List<Mission> GetMissionList();
		public List<MissionTheme> GetMissionThemeList();
		public List<MissionApplication> GetMissionAppList();
		public List<Skill> GetSkillsList();
		public List<Story> GetStoryList();
		public List<Banner> GetBannerList();
		public List<Country> getCountryList();
		public List<Timesheet> GetTimeSheetTimeList();


		// ************** List Section ENd    **********//



		//***********     User Page Method Start   **************///

		// Method for Add User data 
		public bool AddUserDetails(string Ufname, string Ulname, string Uemail, string Upwd, string UphnNumber, string Uavtar, string Uempid, string UDept, string Usts, int Ucountry, int Ucity);

		// Method for get UserData for Edit
		public User GetDataForUserPanel(long uId);

		// Method for Edit User data
		public bool EditDataForUser(string Name, string Surname, string email, string EmployeeId, long userid, string DeptName, string Ustatus);

		public bool EditDataForUserPage(AdminViewModel adminView,long userId);

		// Method for Delete User Data
		public bool RemoveUserData(long  uId);

		//**************   User Page MethodS End    ***************///



		//**************   CMS Page MethodS START    ***************///

		// Method for Add CMS Page Data
		public bool AddCMSpageData(string Title, string Description, string Slug, string Status);

		public CmsPage GetCMSPageDataforEdit(long cmsid);

		public AdminViewModel GetSingleCMsData(long loadCMsid);
		public void EditCMSPageData(AdminViewModel adminView, long cmsPageid);

		// Method for Delete CMS Page Data
		public bool RemoveCMSData(long cmsId);

		//**************   CMS Page Methods END    ***************///




		//**************   Mission Page MethodS START    ***************///



		// Method for Add Mission Data

		public bool AddMissionPagedata(AdminViewModel missionVM, List<long> listOfSkill);

        //public bool SavedMissionData(string mTitle, string mType, DateTime SDate, DateTime EDate, string msts);

		// Method for Get Data for Edit Mission 
		public Mission GetDataForMissionEdit(long mId);
		public Mission GetDataforDocEdit(long mId);

		// Method for Get Edit Mission Data 
		public AdminViewModel GetMissionData(long mId);

		public bool EditMissionPageDatainDB(AdminViewModel adm, List<long> listOfSkill, long mId, List<IFormFile> images, List<IFormFile> Documents);

		//public MissionVMAdmin singleMissionForEdit(long missionId);
		public void EditMissionPageData(AdminViewModel missionVM, long mPageEditId);

        // Method for Delete Mission Data 
        public bool RemoveMissionsData(long missionId);

		//**************   Mission Page Methods END    ***************///




		//**************   MissionTheme Page Methods START    ***************///

		// Method for Add Missiontheme Data
		public bool SaveMisionThemeData(string titleT, DateTime createT, int statusT);

		// Method for Get Data for Missiontheme 
		public MissionTheme GetDataForMissionThemeEdit(long mthemeId);

		// Method for Edit Missiontheme Data
		public void EditDataForMissionTheme(AdminViewModel adminView, long missionThemeid);

		// Method for Delete Missiontheme Data
		public bool RemoveMissionThemeData(long ThId);

		//**************   MissionTheme Page Methods END    ***************///






		//**************   MissionApplication Page Methods START    ***************///

		// Method for Add MissionApplication Data
		public bool SaveMisionApplicationData(long mappMid, long mappUid, DateTime mappADate, DateTime mappCDate);

		// Method for Approved User by Admin
		public bool ApprovedUserbyAdmin(long mAppId);

		// Method for Reject User by Admin
		public bool RejectedUserbyAdmin(long missionAppId);
		//**************   MissionApplication Page Methods END    ***************///



		//**************   MissionSkill Page Methods START    ***************///

		// Method for Add Skills data
		public bool SaveSkillsData(string SName, DateTime SDate /*,string SStatus*/);

		public Skill GetDataForMissionSkillEdit(long mSkillid);

		public void EditSkillPageData(string skillName, long missionSkillid);
		// Method for Delete Skills data
		public bool DeleteSkillByAdmin(long SkillsId);

		//**************   MissionSkill Page Methods END    ***************///




		//**************   Story Page Methods START    ***************///

		public bool StoryApprovedByAdmin(long stId);
		public bool StoryDeclinedByAdmin(long storyid);
		public bool StoryDeletedByAdmin(long dSId);

		//**************   Story Page Methods END    ***************///



		//**************   Banner Page Methods START    ***************///
		public bool AddbannerPageData(AdminViewModel banner);
		//public bool AddBanneData(string textB, string imageB, int sOrderB, DateTime dateB);

		public Banner GetDataForEditBannerPage(long bId);

		//get banner
		public Banner GetBanner(long bId);
		public bool EditBannerPageData(AdminViewModel banner, long bannerId);

		public bool DeleteBannerPageData(long bannerPageId);


		// Timesheet section start
		public bool ApprovedTimeBasedSheet(long timeId);
		public bool RejectTimeBasedSheet(long rejectTid);
		public bool DeleteTimeBasedSheet(long deleteTid);



	}
}
