using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Models;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CIPlatform_Main.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ILandingPage _landingPage;
		private readonly IMissionAndRating _missionAndRating;


        public HomeController(ILogger<HomeController> logger, ILandingPage landingPage, IMissionAndRating missionAndRating)
		{
			_logger = logger;
			_landingPage = landingPage;
			_missionAndRating=missionAndRating;
		}
        public IActionResult LandingPage()
        {
				
            return View();
        }

		[HttpPost]
        public IActionResult LandingPage(string[]? country, string[]? city, string[]? themes, string[]? skills, string? sortVal, string? search, int pg = 1)
		{

			var landingPageData=_landingPage.LandingPage(country,city,themes,skills,sortVal,search,pg);

			LandingPageVM landVM = new()
			{
				MissionThemes = landingPageData.MissionThemes,
				MissionRatings = landingPageData.MissionRatings,
				Skills = landingPageData.Skills,
				GoalMissions = landingPageData.GoalMissions,
				FavoriteMissionList = landingPageData.FavoriteMissionList,
				UserData = landingPageData.UserData,
				Missions = landingPageData.Missions,
				MissionSkills = landingPageData.MissionSkills,
				Countries =landingPageData.Countries,
				Cities=landingPageData.Cities,

			};

			
			return PartialView("_cardsPartialView", landVM);
		
	}



		public IActionResult MissionAndRating(int id)
		{
			var getDataForRelatedMission = _missionAndRating.GetDataForRelatedMission(id);
			return View(getDataForRelatedMission);
		}


		//Rating
		public IActionResult UpdateAndRate(int missionid, int rating, string Email)
		{
			var missionRating = _missionAndRating.UpdateAndRate(missionid, rating, Email);
			return RedirectToAction("RelatedMission", new { id = missionid });
		}

		// Comment
		[HttpPost]
		public IActionResult commentOnMission(int mId, string commentText1)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var commentOnMission = _missionAndRating.commentOnMission(mId, commentText1, uid);
			return RedirectToAction("RelatedMission", new { id = mId });
		}

		//add to favourite mission
		[HttpPost]
		public IActionResult favunfav(int mId)
		{

			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var missionRating = _missionAndRating.favouriteMission(mId, uid);
			return RedirectToAction("RelatedMission", new { id = mId });
		}




		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}