using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Interface
{

  
    public interface IRegistartionRepository
    {
        public bool GetUsers(RegistrationVM registrationVM);
    }
}
