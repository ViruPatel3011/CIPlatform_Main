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

    }
}
