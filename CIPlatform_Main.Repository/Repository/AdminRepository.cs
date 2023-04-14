using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;


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
			var list = _ciPlatformContext.Users.ToList();
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
				_ciPlatformContext.Users.Remove(isValidUser); 
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

	}
}
