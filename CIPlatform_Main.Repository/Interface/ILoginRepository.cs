using CIPlatform_Main.Entities.Models;
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
    }
}
