using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
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
		public List<User> getUserList();
		public List<CmsPage> cmsList();
		public List<Mission> getMissionList();
		public List<MissionTheme> getMissionThemeList();
		public List<MissionApplication> getMissionAppList();
		public List<Skill> getSkillsList();

		public Mission getDataForMissionEdit(long mId);
		public User getDataForUserPanel(long uId);

		public bool EditDataForUser(string Name, string Surname, string email, string EmployeeId, long userid, string DeptName, string Ustatus);

		public bool removeUserData(long  uId);
		public bool removeCMSData(long cmsId);


		public bool AddUserDetails(string Ufname, string Ulname, string Uemail, string Upwd, string UphnNumber, string Uavtar, string Uempid, string UDept, string Usts);

		public bool AddCMSpageData(string Title, string Description, string Slug, string Status);

		//public CmsPage getCMSPageDataforEdit(long cmsid);
		public bool SavedMissionData(string mTitle, string mType, DateTime SDate, DateTime EDate, string msts);

		public void editMissionData(AdminViewModel adminView, long missionid);

		public bool removeMissionsData(long missionId);

		public bool SaveMisionThemeData(string titleT, DateTime createT, int statusT);
		public MissionTheme getDataForMissionThemeEdit(long mthemeId);

		public void EditDataForMissionTheme(AdminViewModel adminView, long missionThemeid);
		public bool removeMissionThemeData(long ThId);

		public bool SaveMisionApplicationData(long mappMid, long mappUid, string mappstatus, DateTime mappADate, DateTime mappCDate);

		public bool ApprovedUserbyAdmin(long mAppId);
		public bool RejectedUserbyAdmin(long missionAppId);


		public bool SaveSkillsData(string SName, DateTime SDate, string SStatus);



	}
}
