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


    public class RegistrationRepository : IRegistartionRepository
    {
        private readonly CiPlatformContext _ciPlatformContext;

        public RegistrationRepository(CiPlatformContext ciPlatformContext)
        {
            _ciPlatformContext = ciPlatformContext;
        }

        public bool GetUsers(RegistrationVM registrationVM)
        {
            var obj = _ciPlatformContext.Users.FirstOrDefault(u => u.Email == registrationVM.Email);
            if (obj == null)
            {
                var regData = new User()
                {
                    Email = registrationVM.Email,
                    FirstName = registrationVM.FirstName,
                    LastName = registrationVM.LastName,
                    PhoneNumber = registrationVM.PhoneNumber,
                    Password = registrationVM.Password,
                    CityId=2,
                    CountryId=1,
                };
                _ciPlatformContext.Users.Add(regData);
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
