using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CIPlatform_Main.Controllers
{
	public class StoryController : Controller
	{

		private readonly IStoryRepository _storyRepository;

		public StoryController(IStoryRepository storyRepository)
		{
			_storyRepository = storyRepository;
		}

		public IActionResult StoryListingPage(storyListingVM svm)
		{
			var allStoryData = _storyRepository.getStoryDetail(svm);
			return View(allStoryData);
		}

        [Authorize]
        public IActionResult shareYourStoryPage(string missionid)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var detailRelatedStory = _storyRepository.getDataForShareYourStory(missionid, uid);

            return View(detailRelatedStory);
        }
        public JsonResult getAllMissions()
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var missions = _storyRepository.getMissions(long.Parse(uid));
            return Json(missions);
        }
        [HttpPost]
        public IActionResult shareYourStoryPage(int mid, string sTitle, string? sDateAndTime, string sDesc, int userId, string[] images, string videoUrl)
        {
            var dataToFillStroyTable = _storyRepository.getDataForStoryTable(mid, sTitle, sDateAndTime, sDesc, userId, images, videoUrl);
            if (dataToFillStroyTable)
            {
                return RedirectToAction("StoryListingPage", "Story");
            }
            else
            {
                TempData["Error"] = "Alredy Post Story";
                return RedirectToAction("StoryListingPage", "Story");
            }


        }



        //method for edit story
        public IActionResult editStory(string missionid)
        {
            return Json(new { redirectUrl = Url.Action("shareYourStoryPage", "Story", new { missionid = missionid }) });
        }


        public IActionResult storyDetail(long userId , int missionId)
        {
            var getStoryDetail = _storyRepository.GetStoryDetail(userId, missionId);
            return View(getStoryDetail);
        }


		// recomandation to co-worker
		public JsonResult GetUsers()
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;

			var userData = _storyRepository.getUsersForRecomandateToCoWorker(uid);

			return Json(userData);
		}
		public string SentUserMail(int[] ids, int missionid)
		{
			string url = Url.Action("MissionAndRating", "Home", new { id = missionid }, Request.Scheme);
			var emailForReco = _storyRepository.userWithId(ids, missionid, url);
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
