using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Interface
{
    public interface ILoginRepository
    {
        public List<User> GetUser();

        public bool AddContactUsData(UserViewModel userView, int uid);


        public List<Admin> getValidAdmin();

        //public List<Banner> getBannerList();



    }
}
