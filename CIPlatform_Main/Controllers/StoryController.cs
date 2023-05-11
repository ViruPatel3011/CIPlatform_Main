using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;

namespace CIPlatform_Main.Controllers
{
	public class StoryController : Controller
	{

		private readonly IStoryRepository _storyRepository;

		public StoryController(IStoryRepository storyRepository)
		{
			_storyRepository = storyRepository;
		}

		public IActionResult StoryListingPage(int pageIndex = 1, int pageSize=1)
		{
			var allStoryData = _storyRepository.GetStoryDetail(pageIndex,pageSize);
			return View(allStoryData);
		}


		// Method Get all the data saved in database for Edit
        [Authorize]
        public IActionResult shareYourStoryPage(string missionid)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var detailRelatedStory = _storyRepository.GetDataForShareYourStory(missionid, uid);
			return View(detailRelatedStory);
        }

		// Method for Get all Mission List in Dropdown
        public JsonResult GetAllMissions()
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var missions = _storyRepository.GetMissions(long.Parse(uid));
            return Json(missions);
        }


		// Method for submit new Story in Database
		[HttpPost]
		public IActionResult shareYourStoryPage(int mid, string sTitle, string? sDateAndTime, string sDesc, int userId, string[] images, string videoUrl)
		{
			var dataToFillStroyTable = _storyRepository.GetDataForStoryTable(mid, sTitle, sDateAndTime, sDesc, userId, images, videoUrl);
			
				return RedirectToAction("StoryListingPage", "Story");
		
		}



		// Method for store images in WWWRoot folder 
		/*	[HttpPost]*/
		/*public IActionResult SaveYourStory(shareYourStoryVM storyVM )
		{

			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var userId = Convert.ToInt32(uid);

			var checkStoryData = _storyRepository.saveStory( storyVM, userId);
			if (checkStoryData)
			{
				return RedirectToAction("StoryListingPage", "Story");
			}
			else
			{
				TempData["Error"] = "Alredy Post Story";
				return RedirectToAction("StoryListingPage", "Story");
			}

		}*/



		//press on submit button
		public IActionResult SubmitStory(long storyid)
		{
			_storyRepository.Submit(storyid);
			return Json(new { redirectUrl = Url.Action("StoryListingPage", "Story") });
		}


		// Method for Edit data for story
		public IActionResult EditShareStory(int mid, string sTitle, string sDesc, int userId, string[] images, string videoUrl)
		{
			var mission_id = mid;

			_storyRepository.EditSharedStory(mission_id, sTitle, sDesc, userId, images, videoUrl);

			return Json(new { redirectUrl = Url.Action("StoryListingPage", "Story") });

		}

		//Edit method called when we select any mission and go to getdataForStory Method
		public IActionResult EditStory(string missionid)
		{
			return Json(new { redirectUrl = Url.Action("shareYourStoryPage", "Story", new { missionid = missionid }) });
		}


		public IActionResult storyDetail(long userId , int missionId,int views,long storyId)
        {
			var count = views + 1;
			var getStoryDetail = _storyRepository.GetStoryDetail(userId, missionId, count, storyId);
            return View(getStoryDetail);
        }


		// recomandation to co-worker
		public JsonResult GetUsers()
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

			var userData = _storyRepository.GetUsersForRecomandateToCoWorker(uid);

			return Json(userData);
		}
		public string SentUserMail(int[] ids, int missionid,long userId)
		{
			string url = Url.Action("MissionAndRating", "Home", new { id = missionid }, Request.Scheme);
			var emailForReco = _storyRepository.UserWithId(ids, missionid, url, userId);
			if (emailForReco != null)
			{
				return "success";
			}
			else
			{
				return "failure";
			}
		}


		public IActionResult Index()
		{
			return View();
		}
	}
}
