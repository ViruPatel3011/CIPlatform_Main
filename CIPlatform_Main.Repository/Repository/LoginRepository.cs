using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly CiPlatformContext _ciPlatformContext;

        public LoginRepository(CiPlatformContext ciPlatformContext)
        {
            _ciPlatformContext = ciPlatformContext;
        }

        public List<User> GetUser()
        {
            List<User> userList = _ciPlatformContext.Users.ToList();
            return userList;
        }

        public bool AddContactUsData(UserViewModel userView, int uid)
        {
            ContactU contactUs = new ContactU() {
                UserId = uid,
                Subject = userView.ContactSubject,
                Message = userView.ContactMessage

            };
            _ciPlatformContext.Add(contactUs);
            _ciPlatformContext.SaveChanges();
            return true;

        }

        public List<Admin> GetValidAdmin()
        {
            List<Admin> adminList = _ciPlatformContext.Admins.ToList();
            return adminList;
        }

        public List<Banner> GetBannerList()
        {
            List<Banner> bannerList = _ciPlatformContext.Banners.ToList();
            return bannerList;
        }

    }
}
