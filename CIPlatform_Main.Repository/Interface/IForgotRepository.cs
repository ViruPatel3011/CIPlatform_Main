using CIPlatform_Main.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Interface
{
    public interface IForgotRepository
    {
        public bool checkUser(ForgotVM forgotVM);
        public bool sendMail(ForgotVM forgotVM,string url);
    }
}
