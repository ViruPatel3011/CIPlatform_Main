using CIPlatform_Main.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Interface
{
    public interface IResetPasswordRepository
    {
        public bool ValidEmail(string Email, string token );

        public bool ValidUser(ResetPasswordVM resetPasswordVM);
    }
}
