using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Models;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace CIPlatform_Main.Controllers
{
	//[Authorize(Roles = "User")]
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
			var navBarData=_landingPage.LandingPageList();
			return View(navBarData);
        }


		[HttpPost]
        public IActionResult LandingPage(string[]? country, string[]? city, string[]? themes, string[]? skills, string? sortVal,string? exploreVal,string? search, int pg = 1)
		{

		
			var landingPageData=_landingPage.LandingPage(country,city,themes,skills,sortVal, exploreVal,search, pg);

			LandingPageVM landVM = new()
			{
				MissionThemes = landingPageData.MissionThemes,
				MissionRatings = landingPageData.MissionRatings,
				Skills = landingPageData.Skills,
				GoalMissions = landingPageData.GoalMissions,
				FavoriteMissionList = landingPageData.FavoriteMissionList,
				UserData = landingPageData.UserData,
				//Missions = landingPageData.Missions,
				MissionSkills = landingPageData.MissionSkills,
				Countries =landingPageData.Countries,
				Cities=landingPageData.Cities,
				MissionMedia=landingPageData.MissionMedia,
				missionApplications=landingPageData.missionApplications,
				TimeSheetList = landingPageData.TimeSheetList



			};

			IEnumerable<Mission> missions =landingPageData.Missions;

			switch (exploreVal)
			{

				case "topthemes":
					missions = missions.OrderByDescending(mission => mission.ThemeId == missions.GroupBy(item => item.ThemeId).OrderByDescending(group => group.Count()).First().Key)
					   .ThenByDescending(mission => mission.ThemeId)
					   .ToList();

					break;
				case "topRanked":
					missions = missions.Where(mission => mission.MissionRatings.Any()).OrderByDescending(mission => mission.MissionRatings.Average(rating => rating.Rating));
					break;
				case "topFavourite":
					missions = missions.OrderByDescending(mission => mission.FavoriteMissions.Count()).ToList();
					break;
				case "RandomMissions":
					var random = new Random();
					missions = missions.OrderBy(mission => random.Next()).Take(3);
					break;





			}


			if (country.Length > 0 || city.Length > 0 || themes.Length > 0 || skills.Length > 0)
			{
				missions = FilterMission(missions, country, city, themes, skills);
			}

			if (search != null)
			{
				missions = missions.Where(m => m.Title.ToLower().Contains(search.ToLower())).ToList();
			}

			missions = SortingData(sortVal, missions);

			

			// This code is for Pagination
			int resCount = missions.Count();
			const int pageSize = 6;
			if (pg < 1)
				pg = 1;
			var pager = new Pager(resCount, pg, pageSize);

			int recSkip = (pg - 1) * pageSize;

			missions = missions.Skip(recSkip).Take(pager.PageSize).ToList();

			this.ViewBag.userPager = pager;

			ViewBag.missionCount = resCount;

			landVM.Missions = missions;
			return PartialView("_cardsPartialView", landVM);
		
	}


		// Function for sorting Mission-cards on input dropdown
		public IEnumerable<Mission> SortingData(string sortVal, IEnumerable<Mission> missions)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			switch (sortVal)
			{

				case "Newest":
					return missions.OrderByDescending(p => p.StartDate).ToList();
				case "Oldest":
					return missions.OrderBy(p => p.StartDate).ToList();
				case "LAS":
					return missions.OrderBy(p => p.TotalSeats).ToList();
				case "HAS":
					return missions.OrderByDescending(p => p.TotalSeats).ToList();
				case "Goal":
					return missions.Where(m => m.MissionType.Equals("Goal")).ToList();
				case "Time":
					return missions.Where(m => m.MissionType.Equals("Time")).ToList();
				//case "Favourite":
				//	return missions.OrderByDescending(mission=>mission.FavoriteMissions).Any()
				default:
					return missions.ToList();
			}
		}


		public IEnumerable<Mission> FilterMission(IEnumerable<Mission> missions, string[]? country, string[]? city, string[]? themes, string[]? skills)
		{
			if (country.Length > 0)
			{
				missions = missions.Where(s => country.Contains(s.Country.Name)).ToList();
			}
			if (city.Length > 0)
			{
				missions = missions.Where(s => city.Contains(s.City.Name)).ToList();
			}
			if (themes.Length > 0)
			{
				missions = missions.Where(s => themes.Contains(s.Theme.Title)).ToList();
			}
			if (skills.Length > 0)
			{
				missions = missions.Where(mission => mission.MissionSkills.Any(skill => skills.Contains(skill.Skill.SkillName))).ToList();
			}
			return missions;
		}


		public IActionResult MissionAndRating(int id)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var getDataForRelatedMission = _missionAndRating.GetDataForRelatedMission(id,Convert.ToInt32(uid));
			return View(getDataForRelatedMission);
		}


		//Rating
		public IActionResult UpdateAndRate(int missionid, int rating, string Email)
		{
			var missionRating = _missionAndRating.UpdateAndRate(missionid, rating, Email);
			return RedirectToAction("MissionAndRating", new { id = missionid });
		}

		// Comment
		[HttpPost]
		public IActionResult CommentOnMission(int mId, string commentText1)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var commentOnMission = _missionAndRating.CommentedOnmission(mId, commentText1, uid);
			return RedirectToAction("MissionAndRating", new { id = mId });
		}

		//add to favourite mission
		[HttpPost]
		public IActionResult DoFavouriteMission(int mId)
		{

			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

			var missionRating = _missionAndRating.MissionFavourite(mId, uid);
			return RedirectToAction("MissionAndRating", new { id = mId });
		}

		// recomandation to co-worker
		public JsonResult GetUsers()
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

			var userData = _missionAndRating.GetUsersForRecomandateToCoWorker(uid);

			return Json(userData);
		}
		public string SentUserMail(int[] ids, int missionid)
		{
			string url = Url.Action("MissionAndRating", "Home", new { id = missionid }, Request.Scheme);
			var emailForReco = _missionAndRating.UserWithId(ids, missionid, url);
			if (emailForReco != null)
			{
				return "success";
			}
			else
			{
				return "failure";
			}
		}


		public IActionResult AppliedForMission(int mId)
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

			//var alreadyApplied = _ciPlatformContext.MissionApplications.Where(x => x.MissionId == mId && x.UserId == Convert.ToInt32(uid)).FirstOrDefault();
			var alreadyApplied = _missionAndRating.AlreadyApplied(mId, uid);

			return RedirectToAction("MissionAndRating", new { id = mId });
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