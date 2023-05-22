using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CIPlatform_Main.Controllers
{
	public class NotificationController : Controller
	{



		private readonly INotification _notification;

		public NotificationController (INotification notification)
		{
			_notification = notification;	
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult AddCheckedUserNotification(long[] selectedUserCheckedValues, long userid)
		{
			_notification.AddCheckedNotification(selectedUserCheckedValues, userid);
			TempData["Success Message"] = "Data Edited Successfully";
			return RedirectToAction("LandingPage", "Home");
		}

		public JsonResult ShowUserNotification(long userId)
		{
			var getRelatedNotification = _notification.ShowSpecificUserNotification(userId);
			return Json(getRelatedNotification);
		}

		public IActionResult ReadNotification(long messageid)
		{
			_notification.MarkAsRead(messageid);
			return RedirectToAction("LandingPage", "Home");
		}
	}
}
