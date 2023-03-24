using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Repository
{
    public class ResetPasswordRepository:IResetPasswordRepository
    {
        private readonly CiPlatformContext _ciPlatformContext;

        public ResetPasswordRepository(CiPlatformContext ciPlatformContext)
        {
            _ciPlatformContext = ciPlatformContext;
       }


        public bool ValidEmail(string Email, string Token)
        {
            var isValidEmail = _ciPlatformContext.PasswordResets.FirstOrDefault(u => u.Email == Email && u.Token == Token);
            if(isValidEmail != null) {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool ValidUser(ResetPasswordVM resetPasswordVM)
        {
            var isValidUser = _ciPlatformContext.Users.FirstOrDefault(u => u.Email == resetPasswordVM.Email);
            if(isValidUser != null)
            {
                isValidUser.Password = resetPasswordVM.Password;
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
