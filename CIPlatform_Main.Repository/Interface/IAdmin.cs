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
		public List<User> getUserList();
		public List<CmsPage> cmsList();
		public List<Mission> getMissionList();
		public List<MissionTheme> getMissionThemeList();
		public List<MissionApplication> getMissionAppList();
		public List<Skill> getSkillsList();
		public List<Story> getStoryList();
		public List<Banner> getBannerList();
		public List<Country> getCountryList();


		// ************** List Section ENd    **********//



		//***********     User Page Method Start   **************///

		// Method for Add User data 
		public bool AddUserDetails(string Ufname, string Ulname, string Uemail, string Upwd, string UphnNumber, string Uavtar, string Uempid, string UDept, string Usts);

		// Method for get UserData for Edit
		public User getDataForUserPanel(long uId);

		// Method for Edit User data
		public bool EditDataForUser(string Name, string Surname, string email, string EmployeeId, long userid, string DeptName, string Ustatus);

		// Method for Delete User Data
		public bool removeUserData(long  uId);

		//**************   User Page MethodS End    ***************///



		//**************   CMS Page MethodS START    ***************///

		// Method for Add CMS Page Data
		public bool AddCMSpageData(string Title, string Description, string Slug, string Status);

		public CmsPage getCMSPageDataforEdit(long cmsid);

		public void EditCMSPageData(AdminViewModel adminView, long cmsPageid);

		// Method for Delete CMS Page Data
		public bool removeCMSData(long cmsId);

		//**************   CMS Page Methods END    ***************///




		//**************   Mission Page MethodS START    ***************///



		// Method for Add Mission Data

		public bool AddMissionPagedata(MissionVMAdmin missionVM, List<long> listOfSkill);

        //public bool SavedMissionData(string mTitle, string mType, DateTime SDate, DateTime EDate, string msts);

		// Method for Get Data for Edit Mission 
		public Mission getDataForMissionEdit(long mId);

		// Method for Edit Mission Data 

		public void editMissionPageData(MissionVMAdmin missionVM, long mPageEditId);

        // Method for Delete Mission Data 
        public bool removeMissionsData(long missionId);

		//**************   Mission Page Methods END    ***************///




		//**************   MissionTheme Page Methods START    ***************///

		// Method for Add Missiontheme Data
		public bool SaveMisionThemeData(string titleT, DateTime createT, int statusT);

		// Method for Get Data for Missiontheme 
		public MissionTheme getDataForMissionThemeEdit(long mthemeId);

		// Method for Edit Missiontheme Data
		public void EditDataForMissionTheme(AdminViewModel adminView, long missionThemeid);

		// Method for Delete Missiontheme Data
		public bool removeMissionThemeData(long ThId);

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

		// Method for Delete Skills data
		public bool DeleteSkillByAdmin(long SkillsId);

		//**************   MissionSkill Page Methods END    ***************///




		//**************   Story Page Methods START    ***************///

		public bool StoryApprovedByAdmin(long stId);
		public bool StoryDeclinedByAdmin(long storyid);
		public bool StoryDeletedByAdmin(long dSId);

		//**************   Story Page Methods END    ***************///



		//**************   Banner Page Methods START    ***************///
		public bool AddbannerPageData(BannerVM banner);
		//public bool AddBanneData(string textB, string imageB, int sOrderB, DateTime dateB);

		public Banner getDataForEditBannerPage(long bId);

		//get banner
		public Banner getBanner(long bId);
		public bool EditBannerPageData(BannerVM banner, long bannerId);

		public bool deleteBannerPageData(long bannerPageId);
      
    }
}
