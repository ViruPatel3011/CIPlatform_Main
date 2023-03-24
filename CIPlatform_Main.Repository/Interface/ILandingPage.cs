using CIPlatform_Main.Entities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform_Main.Repository.Interface
{
    public interface ILandingPage
    {
        public LandingPageVM GetLandingPageData(string[]? country, string[]? city, string[]? themes, string[]? skills, string? sortVal, string? search, int pg = 1); 
    }
}
