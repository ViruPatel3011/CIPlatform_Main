using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CIPlatform_Main.Controllers
{
	public class UserController : Controller
	{


		private readonly IUserRepository _userRepository;

		public UserController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

	
		public IActionResult UserProfile()
		{
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var userData = _userRepository.getUserData(Convert.ToInt32(uid));
			return View(userData);
		
		}

		[HttpPost]
		public IActionResult SaveUserData(UserViewModel userView) {
			var identity = User.Identity as ClaimsIdentity;
			var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
			var userData = _userRepository.SaveUserProfile(userView,Convert.ToInt32(uid));
			return RedirectToAction("UserProfile", "User");

		}
		public IActionResult Index()
		{
			return View();
		}
	}
}
