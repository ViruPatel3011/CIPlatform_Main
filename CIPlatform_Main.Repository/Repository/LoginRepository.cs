using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
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
            List<User> userList=_ciPlatformContext.Users.ToList();
            return userList;
        }

    }
}
