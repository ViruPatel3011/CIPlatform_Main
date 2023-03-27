using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
		public IActionResult shareYourStoryPage(storyListingVM svm)
		{
			var detailRelatedStory = _storyRepository.getStoryDetail(svm);

			return View(detailRelatedStory);
		}


		[HttpPost]
		public IActionResult shareYourStoryPage(int MissionId, string StoryTitle, string StoryText, DateTime StoryDate, int userId)
		{
			var dataToFillStroyTable = _storyRepository.getDataForStoryTable(MissionId, StoryTitle, StoryText, StoryDate, userId);
			return RedirectToAction("StoryListingPage", "Story");

		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
