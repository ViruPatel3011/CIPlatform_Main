using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Models;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CIPlatform_Main.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ILandingPage _landingPage;


        public HomeController(ILogger<HomeController> logger, ILandingPage landingPage)
		{
			_logger = logger;
			_landingPage = landingPage;
		}
		
		public IActionResult LandingPage(string[]? country, string[]? city, string[]? themes, string[]? skills, string? sortVal, string? search, int pg = 1)
		{

			var landingPageData=_landingPage.GetLandingPageData(country,city,themes,skills,sortVal,search,pg);
			return PartialView("_cardsPartialView", landingPageData);
		
	}
		
		
		
		public IActionResult Index()
		{
			return View();
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