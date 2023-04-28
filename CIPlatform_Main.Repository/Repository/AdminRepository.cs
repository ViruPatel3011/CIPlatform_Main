using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace CIPlatform_Main.Repository.Repository
{
	public class AdminRepository : IAdmin
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
			var list = _ciPlatformContext.Users.OrderBy(x => x.Status).ToList();
			return list;
		}
		public List<CmsPage> cmsList()
		{
			var list = _ciPlatformContext.CmsPages.OrderBy(x => x.Status).ToList();
			return list;
		}
		public List<Mission> getMissionList()
		{
			var list = _ciPlatformContext.Missions.OrderBy(x => x.Status).ToList();
			return list;
		}

		public List<MissionTheme> getMissionThemeList()
		{
			var list = _ciPlatformContext.MissionThemes.ToList();
			return list;
		}
		public List<MissionApplication> getMissionAppList()
		{
			var list = _ciPlatformContext.MissionApplications.Where(x => x.ApprovalStatus != "Rejected").OrderBy(x => x.ApprovalStatus).ToList();
			//var list = _ciPlatformContext.MissionApplications.OrderBy(x=>x.ApprovalStatus).ToList();
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
		public List<Country> getCountryList()
		{
			var list = _ciPlatformContext.Countries.ToList();
			return list;
		}


		// ************** List Section ENd    **********//



		//**************   User Page Methods START    ***************///

		// Method for Add User data
		public bool AddUserDetails(string Ufname, string Ulname, string Uemail, string Upwd, string UphnNumber, string Uavtar, string Uempid, string UDept, string Usts)
		{
			var checkUser = _ciPlatformContext.Users.Where(x => x.Email == Uemail).FirstOrDefault();
			if (checkUser != null)
			{
				return false;
			}
			else
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


		}

		// Method for get UserData for Edit
		public User getDataForUserPanel(long uId)
		{
			var checkUser = _ciPlatformContext.Users.Where(x => x.UserId == uId).FirstOrDefault();
			return checkUser;
		}


		// Method for Edit User data
		public bool EditDataForUser(string Name, string Surname, string email, string EmployeeId, long userid, string DeptName, string Ustatus)
		{
			var isUserValid = _ciPlatformContext.Users.Where(x => x.UserId == userid).FirstOrDefault();
			isUserValid.FirstName = Name;
			isUserValid.LastName = Surname;
			isUserValid.Email = email;
			isUserValid.EmployeeId = EmployeeId;
			isUserValid.Department = DeptName;
			isUserValid.Status = Ustatus;
			_ciPlatformContext.SaveChanges();
			return true;
		}

		public bool EditDataForUserPage(AdminViewModel adminView, long userId)
		{
			var user = _ciPlatformContext.Users.Where(x => x.UserId == userId).FirstOrDefault();
			user.FirstName = adminView.FirstName;
			user.LastName = adminView.LastName;
			//user.Email=adminView.UEmail;	
			user.EmployeeId = adminView.EmployeeId;
			user.Department = adminView.Department;
			user.Status = adminView.Status;
			_ciPlatformContext.Users.Update(user);
			_ciPlatformContext.SaveChanges();
			return true;

		}

		// Method for Remove User data
		public bool removeUserData(long uId)
		{
			var isValidUser = _ciPlatformContext.Users.Where(x => x.UserId == uId).FirstOrDefault();
			isValidUser.Status = "Deactive";
			isValidUser.DeletedAt = DateTime.Now;
			var userAlreadyInUse = _ciPlatformContext.MissionApplications.Where(x => x.UserId == isValidUser.UserId).FirstOrDefault();

			if (userAlreadyInUse == null)
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

		public CmsPage getCMSPageDataforEdit(long cmsid)
		{
			var cmspage = _ciPlatformContext.CmsPages.Where(x => x.CmsPageId == cmsid).FirstOrDefault();
			return cmspage;
		}

		public AdminViewModel getSingleCMsData(long cmsId)
		{
			var cmsPage = _ciPlatformContext.CmsPages.Where(cms => cms.CmsPageId == cmsId).FirstOrDefault();
			AdminViewModel admin = new AdminViewModel()
			{
				CMSTitle = cmsPage.Title,
				CMSDescrition = cmsPage.Description,
				CMSSlug = cmsPage.Slug,
				CMSStatus = cmsPage.Status
			};
			return admin;

		}

		public void EditCMSPageData(AdminViewModel adminView, long cmsPageid)
		{
			var checkCMSId = _ciPlatformContext.CmsPages.Where(x => x.CmsPageId == cmsPageid).FirstOrDefault();
			checkCMSId.Title = adminView.CMSTitle;
			checkCMSId.Description = adminView.CMSDescrition;
			checkCMSId.Slug = adminView.CMSSlug;
			checkCMSId.Status = adminView.CMSStatus;
			_ciPlatformContext.CmsPages.Update(checkCMSId);
			_ciPlatformContext.SaveChanges();
		}

		// Method for Delete CMS Page Data
		public bool removeCMSData(long cmsId)
		{
			var cmsDataExist = _ciPlatformContext.CmsPages.Where(x => x.CmsPageId == cmsId).FirstOrDefault();
			if (cmsDataExist != null)
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
		public bool AddMissionPagedata(AdminViewModel missionVM, List<long> listOfSkill)
		{
			Mission mission = new Mission()
			{

				Title = missionVM.title,
				Description = missionVM.description,
				ShortDescription = missionVM.shortDescription,
				CountryId = missionVM.countryId,
				CityId = missionVM.cityId,
				OrganizationName = missionVM.organizationName,
				OrganizationDetail = missionVM.organizationDetail,
				StartDate = missionVM.startDate,
				EndDate = missionVM.endDate,
				MissionType = missionVM.missionType,
				Availability = missionVM.availability,
				ThemeId = missionVM.themeId,


			};
			_ciPlatformContext.Missions.Add(mission);
			_ciPlatformContext.SaveChanges();

			var lastMissionId = _ciPlatformContext.Missions.OrderByDescending(m => m.MissionId).Select(m => m.MissionId).FirstOrDefault();
			//         foreach (var image in missionVM.images)
			//         {
			//	var fileName = image.FileName;
			//	var fileType = image.ContentType;

			//	using (var fileStream = image.OpenReadStream())
			//	{
			//		var filePath = Path.Combine("MissionImgDcouments", fileName);
			//		using (var fStream = new FileStream(Path.Combine("wwwroot", "MissionImgDcouments", fileName), FileMode.Create))
			//		{
			//			image.CopyToAsync(fStream);
			//			var missionMedia = new MissionMedium
			//			{
			//				MissionId = mission.MissionId,
			//				MediaPath = filePath,
			//				MediaName=image.FileName,
			//				MediaType=image.ContentType


			//			};

			//			_ciPlatformContext.MissionMedia.Add(missionMedia);
			//		}
			//	}

			//}

			//foreach (var documents in missionVM.Documents)
			//{
			//	var fileName = documents.FileName;
			//	var fileType = documents.ContentType;

			//	using (var fileStream = documents.OpenReadStream())
			//	{
			//		var filePath = Path.Combine("MissionImgDcouments", fileName);
			//		using (var fStream = new FileStream(Path.Combine("wwwroot", "MissionImgDcouments", fileName), FileMode.Create))
			//		{
			//			documents.CopyToAsync(fStream);
			//			var missiondocuments = new MissionDocument
			//			{
			//				MissionId = mission.MissionId,
			//				DocumentName = documents.FileName,
			//				DocumentType = fileType,
			//				DocumentPath = filePath,
			//				CreatedAt = DateTime.Now,
			//			};

			//			_ciPlatformContext.MissionDocuments.Add(missiondocuments);
			//		}
			//	}
			//}

			foreach (var image in missionVM.images)
			{
				var fileName = image.FileName;
				var fileType = image.ContentType;
				byte[] imageData;

				using (var fileStream = image.OpenReadStream())
				{
					using (var memoryStream = new MemoryStream())
					{
						fileStream.CopyTo(memoryStream);
						imageData = memoryStream.ToArray();
					}
				}
				var base64String = Convert.ToBase64String(imageData);
				var missionMedia = new MissionMedium
				{
					MissionId = mission.MissionId,
					MediaPath = $"data:{fileType};base64,{base64String}",
					MediaName = fileName,
					MediaType = fileType
				};

				_ciPlatformContext.MissionMedia.Add(missionMedia);
			}

			foreach (var document in missionVM.Documents)
			{
				var fileName = document.FileName;
				var fileType = document.ContentType;

				using (var stream = document.OpenReadStream())
				{
					byte[] fileBytes;
					using (var ms = new MemoryStream())
					{
						stream.CopyTo(ms);
						fileBytes = ms.ToArray();
					}

					var base64String = Convert.ToBase64String(fileBytes);
					var missionDocument = new MissionDocument
					{
						MissionId = mission.MissionId,
						DocumentName = fileName,
						DocumentType = fileType,
						DocumentPath = "data:" + fileType + ";base64," + base64String,
						CreatedAt = DateTime.Now
					};
					_ciPlatformContext.MissionDocuments.Add(missionDocument);
				}
			}




			foreach (var skillAdd in listOfSkill)
			{
				var missionSkill = new MissionSkill
				{
					MissionId = mission.MissionId,
					SkillId = skillAdd
				};
				_ciPlatformContext.MissionSkills.Add(missionSkill);
			}

			// Save changes to the database
			_ciPlatformContext.SaveChanges();

			return true;

		}


		//public MissionVMAdmin singleMissionForEdit(long missionId)
		//{
		//	var thisMission = _ciPlatformContext.Missions.FirstOrDefault(mission => mission.MissionId == missionId);
		//	var missionImages=_ciPlatformContext.MissionMedia.Where(img=>img.MissionId== missionId).ToList();	
		//	var missionDocs=_ciPlatformContext.MissionDocuments.Where(doc=>doc.MissionId== missionId).ToList();

		//	MissionVMAdmin missionVM = new MissionVMAdmin()
		//	{

		//		title=thisMission.Title,
		//		shortDescription=thisMission.ShortDescription,
		//		description =thisMission.Description,
		//		cityId = thisMission.CityId,
		//		countryId = thisMission.CountryId,
		//		themeId = thisMission.ThemeId,
		//		organizationName = thisMission.OrganizationName,
		//		organizationDetail = thisMission.OrganizationDetail,
		//		startDate = thisMission.StartDate,
		//		endDate = thisMission.EndDate,
		//		missionType = thisMission.MissionType,	
		//		availability = thisMission.Availability,	
		//		MissionList=getMissionList(),
		//		MissionThemeList=getMissionThemeList(),
		//		SkillList=getSkillsList()



		//	};
		//	return missionVM;

		//}





		public Mission getDataForMissionEdit(long mId)
		{
			//var mission = _ciPlatformContext.Missions.Where(x => x.MissionId == mId).FirstOrDefault();
			var mission = _ciPlatformContext.Missions.Include(m => m.MissionMedia).FirstOrDefault(m => m.MissionId == mId);
			return mission;
		}
		public Mission getDataforDocEdit(long mId)
		{
			//var mission = _ciPlatformContext.Missions.Where(x => x.MissionId == mId).FirstOrDefault();
			var mission = _ciPlatformContext.Missions.Include(m => m.MissionDocuments).FirstOrDefault(m => m.MissionId == mId);
			return mission;
		}

		public AdminViewModel getMissionData(long mId)
		{
			var currentMission = _ciPlatformContext.Missions.Where(m => m.MissionId == mId).FirstOrDefault();
			var missionDoc = _ciPlatformContext.MissionDocuments.Where(md => md.MissionId == mId).Select(md => md.DocumentPath).ToList();
			var missionImages = _ciPlatformContext.MissionMedia.Where(md => md.MissionId == mId).Select(mi => mi.MediaPath).ToList();
			var missionSkill = _ciPlatformContext.MissionSkills.Where(ms => ms.MissionId == mId).ToList();
			var listOfUser = _ciPlatformContext.Users.ToList();
			AdminViewModel adminView = new AdminViewModel()
			{

				title = currentMission.Title,
				shortDescription = currentMission.ShortDescription,
				description = currentMission.Description,
				organizationName = currentMission.OrganizationName,
				missionType = currentMission.MissionType,
				organizationDetail = currentMission.OrganizationDetail,
				startDate = (DateTime)currentMission.StartDate,
				endDate = (DateTime)currentMission.EndDate,
				countryId = currentMission.CountryId,
				cityId = currentMission.CityId,
				themeId = currentMission.ThemeId,
				availability = currentMission.Availability,
				editMissionImages = missionImages.ToList(),
				editMissionDocuments = missionDoc.ToList(),
				MissionThemeList = listOfTheme(),
				SkillsList = listOfSkill(),
				CountryList = listOfCountry(),
				CityList = listOfCity(),
				MissionSkillList = missionSkill.ToList(),
				UserList = listOfUser,
				missionId = mId,
			};
			return adminView;
		}

		public bool EditMissionPageDatainDB(AdminViewModel adm, List<long> listOfSkill, long mId, List<IFormFile> images, List<IFormFile> Documents)
		{
			var dataEdit = _ciPlatformContext.Missions.Where(mission => mission.MissionId == mId).FirstOrDefault();
			dataEdit.Title = adm.title;
			dataEdit.ShortDescription = adm.shortDescription;
			dataEdit.Description = adm.description;
			dataEdit.OrganizationName = adm.organizationName;
			dataEdit.MissionType = adm.missionType;
			dataEdit.OrganizationDetail = adm.organizationDetail;
			dataEdit.StartDate = adm.startDate;
			dataEdit.EndDate = adm.endDate;
			dataEdit.CountryId = adm.countryId;
			dataEdit.CityId = adm.cityId;
			dataEdit.ThemeId = adm.themeId;
			dataEdit.Availability = adm.availability;

			_ciPlatformContext.Missions.Update(dataEdit);
			_ciPlatformContext.SaveChanges();


			var listofImage = _ciPlatformContext.MissionMedia.ToList();
			foreach (var image in listofImage)
			{
				if (mId == image.MissionId)
				{
					_ciPlatformContext.MissionMedia.Remove(image);
				}
			}
			_ciPlatformContext.SaveChanges();


			// Below code is for Images Store in database
			foreach (var image in images)
			{
				var fileName = image.FileName;
				var fileType = image.ContentType;
				byte[] imageData;

				using (var fileStream = image.OpenReadStream())
				{
					using (var memoryStream = new MemoryStream())
					{
						fileStream.CopyTo(memoryStream);
						imageData = memoryStream.ToArray();
					}
				}
				var base64String = Convert.ToBase64String(imageData);
				var missionMedia = new MissionMedium
				{
					MissionId = dataEdit.MissionId,
					MediaPath = $"data:{fileType};base64,{base64String}",
					MediaName = fileName,
					MediaType = fileType,
					UpdatedAt = DateTime.Now
				};

				_ciPlatformContext.MissionMedia.Add(missionMedia);
			}



			var listofDoc = _ciPlatformContext.MissionDocuments.ToList();
			foreach (var doc in listofDoc)
			{
				if (mId == doc.MissionId)
				{
					_ciPlatformContext.MissionDocuments.Remove(doc);
				}
			}
			_ciPlatformContext.SaveChanges();


			// Below code is for Documents Store in database
			foreach (var document in Documents)
			{
				var fileName = document.FileName;
				var fileType = document.ContentType;

				using (var stream = document.OpenReadStream())
				{
					byte[] fileBytes;
					using (var ms = new MemoryStream())
					{
						stream.CopyTo(ms);
						fileBytes = ms.ToArray();
					}

					var base64String = Convert.ToBase64String(fileBytes);
					var missionDocument = new MissionDocument
					{
						MissionId = dataEdit.MissionId,
						DocumentName = fileName,
						DocumentType = fileType,
						DocumentPath = "data:" + fileType + ";base64," + base64String,
						CreatedAt = DateTime.Now
					};
					_ciPlatformContext.MissionDocuments.Add(missionDocument);
				}
			}

			var missionSkill = _ciPlatformContext.MissionSkills.ToList();
			foreach(var skill in missionSkill)
			{
				if (skill.MissionId == mId)
				{
					_ciPlatformContext.MissionSkills.Remove(skill);
				}
			}

			_ciPlatformContext.SaveChanges();

			foreach(var skills in listOfSkill)
			{
				MissionSkill mSkill = new MissionSkill()
				{
					SkillId = skills,
					MissionId = mId,
					UpdatedAt = DateTime.Now,
				};
				_ciPlatformContext.MissionSkills.Add(mSkill);
			}
			_ciPlatformContext.SaveChanges();

			return true;

		}
		public List<MissionTheme> listOfTheme()
		{
			return _ciPlatformContext.MissionThemes.ToList();
		}
		public List<Skill> listOfSkill()
		{
			return _ciPlatformContext.Skills.ToList();
		}
		public List<Country> listOfCountry()
		{
			return _ciPlatformContext.Countries.ToList();
		}
		public List<City> listOfCity()
		{
			return _ciPlatformContext.Cities.ToList();
		}

		// Method for Edit Mission Data 
		public void editMissionPageData(AdminViewModel missionVM, long mPageEditId)
		{
			var editMissionId = _ciPlatformContext.Missions.Where(x => x.MissionId == mPageEditId).FirstOrDefault();
			editMissionId.Title = missionVM.title;
			editMissionId.Description = missionVM.description;
			editMissionId.ShortDescription = missionVM.shortDescription;
			editMissionId.CountryId = missionVM.countryId;
			editMissionId.CityId = missionVM.cityId;
			editMissionId.OrganizationName = missionVM.organizationName;
			editMissionId.OrganizationDetail = missionVM.organizationDetail;
			editMissionId.StartDate = missionVM.startDate;
			editMissionId.EndDate = missionVM.endDate;
			editMissionId.MissionType = missionVM.missionType;
			editMissionId.ThemeId = missionVM.themeId;
			editMissionId.Availability = missionVM.availability;
			editMissionId.Availability = missionVM.availability;
			_ciPlatformContext.Missions.Update(editMissionId);
			_ciPlatformContext.SaveChanges();


		}

		// Method for Delete Mission Data 
		public bool removeMissionsData(long missionId)
		{
			var validMission = _ciPlatformContext.Missions.Where(x => x.MissionId == missionId).FirstOrDefault();
			if (validMission != null)
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
		public bool SaveMisionThemeData(string titleT, DateTime createT, int statusT)
		{
			MissionTheme missionTheme = new MissionTheme()
			{
				//MissionThemeId = IdT,
				Title = titleT,
				CreatedAt = createT,
				Status = (byte)statusT
			};
			_ciPlatformContext.MissionThemes.Add(missionTheme);
			_ciPlatformContext.SaveChanges();
			return true;
		}

		// Method for Get Data for Missiontheme 
		public MissionTheme getDataForMissionThemeEdit(long mthemeId)
		{
			var missionTheme = _ciPlatformContext.MissionThemes.Where(x => x.MissionThemeId == mthemeId).FirstOrDefault();
			return missionTheme;
		}

		// Method for Edit Missiontheme Data
		public void EditDataForMissionTheme(AdminViewModel adminView, long missionThemeid)
		{
			var theme = _ciPlatformContext.MissionThemes.Where(x => x.MissionThemeId == missionThemeid).FirstOrDefault();
			theme.Title = adminView.themeTitle;
			theme.CreatedAt = adminView.createdDate;
			theme.Status = adminView.themeStatus;
			_ciPlatformContext.MissionThemes.Update(theme);
			_ciPlatformContext.SaveChanges();

		}

		// Method for Delete Missiontheme Data
		public bool removeMissionThemeData(long ThId)
		{
			var themeId = _ciPlatformContext.MissionThemes.Where(x => x.MissionThemeId == ThId).FirstOrDefault();
			var missionList = _ciPlatformContext.Missions.ToList();
			foreach (var missionTheme in missionList)
			{
				if (missionTheme.ThemeId == themeId.MissionThemeId)
				{
					return false;
				}
			}
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
		public bool SaveMisionApplicationData(long mappMid, long mappUid, DateTime mappADate, DateTime mappCDate)
		{
			MissionApplication missionapp = new MissionApplication()
			{
				MissionId = mappMid,
				UserId = mappUid,
				ApprovalStatus = "Pending",
				AppliedAt = mappADate,
				CreatedAt = mappCDate,


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
			Skill skill = new Skill()
			{

				SkillName = SName,
				CreatedAt = SDate,
				//Status=SStatus
				Status = "Active"

			};
			_ciPlatformContext.Skills.Add(skill);
			_ciPlatformContext.SaveChanges();
			return true;

		}

		public Skill getDataForMissionSkillEdit(long mSkillid)
		{
			var skillid = _ciPlatformContext.Skills.Where(skill => skill.SkillId == mSkillid).FirstOrDefault();
			return skillid;
		}

		[HttpPost]
		public void EditSkillPageData(string skillName, long missionSkillid)
		{
			var skillEdit = _ciPlatformContext.Skills.Where(skill => skill.SkillId == missionSkillid).FirstOrDefault();
			skillEdit.SkillName = skillName;
			skillEdit.UpdatedAt = DateTime.Now;
			_ciPlatformContext.Skills.Update(skillEdit);
			_ciPlatformContext.SaveChanges();

		}
		// Method for Delete Skills data
		public bool DeleteSkillByAdmin(long SkillsId)
		{
			var skill = _ciPlatformContext.Skills.Where(x => x.SkillId == SkillsId).FirstOrDefault();
			var skillList = _ciPlatformContext.MissionSkills.ToList();
			foreach (var skillDelete in skillList)
			{
				if (skillDelete.SkillId == skill.SkillId)
				{
					return false;
				}
			}
			if (skill != null)
			{
				skill.Status = "DeActive";
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
			var storyid = _ciPlatformContext.Stories.Where(x => x.StoryId == stId).FirstOrDefault();
			if (storyid != null)
			{
				storyid.Status = "Approved";
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
				storyId.DeletedAt = DateTime.Now;
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
		public bool AddbannerPageData(AdminViewModel banner)
		{
			byte[] imageData;
			string base64ImageString = null;
			if (banner.bannerImage != null && banner.bannerImage.Length > 0)
			{
				// Read the image data into a byte array
				using (var ms = new MemoryStream())
				{
					banner.bannerImage.CopyTo(ms);
					imageData = ms.ToArray();
				}

				// Encode the image data as a base64 string
				base64ImageString = Convert.ToBase64String(imageData);
			}

			Banner ban = new Banner()
			{
				Image = base64ImageString, // Use the base64 string instead of the file path
				Text = banner.bannerText,
				SortOrder = banner.sortOrder,
				CreatedAt = DateTime.Now,
			};

			if (_ciPlatformContext.Banners.Any(b => b.SortOrder == banner.sortOrder))
			{
				return false; // Return false if sortOrder already exists
			}

			_ciPlatformContext.Banners.Add(ban);
			_ciPlatformContext.SaveChanges();
			return true;
		}

		//public bool AddBanneData(string textB, string imageB, int sOrderB, DateTime dateB)
		//{
		//	Banner banner = new Banner()
		//	{ 
		//		Text=textB,
		//		Image=imageB,
		//		SortOrder=sOrderB,
		//		CreatedAt=dateB

		//	};
		//	_ciPlatformContext.Banners.Add(banner); 
		//	_ciPlatformContext.SaveChanges();
		//	return true;

		//}

		//get banner
		public Banner getBanner(long bId)
		{
			return _ciPlatformContext.Banners.FirstOrDefault(bnr => bnr.BannerId == bId);
		}
		public Banner getDataForEditBannerPage(long bId)
		{
			var bannerList = _ciPlatformContext.Banners.Where(x => x.BannerId == bId).FirstOrDefault();
			return bannerList;
		}

		public bool EditBannerPageData(AdminViewModel banner, long bannerId)
		{
			var bannerPageId = _ciPlatformContext.Banners.Where(x => x.BannerId == bannerId).FirstOrDefault();
			var fileName = banner.bannerImage?.FileName;
			//var fileType = advm.bannerImage.ContentType;

			using (var fileStream = banner.bannerImage.OpenReadStream())
			{
				var filePath = Path.Combine("\\MissionImgDcouments", fileName);
				using (var fStream = new FileStream(Path.Combine("wwwroot", "MissionImgDcouments", fileName), FileMode.Create))
				{
					banner.bannerImage.CopyTo(fStream);
					if (bannerPageId != null)
					{

						bannerPageId.Text = banner.bannerText;
						bannerPageId.Image = filePath;
						bannerPageId.SortOrder = banner.sortOrder;
						bannerPageId.CreatedAt = banner.CreatedAt;
						_ciPlatformContext.Banners.Update(bannerPageId);
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

		public bool deleteBannerPageData(long bannerPageId)
		{
			var bid = _ciPlatformContext.Banners.Where(x => x.BannerId == bannerPageId).FirstOrDefault();
			if (bid != null)
			{

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
