using CIPlatform_Main.Entities.Models;
using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;

using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CIPlatform_Main.Controllers
{
	public class AdminController : Controller
	{


		private readonly IAdmin _admin;

		public AdminController(IAdmin admin)
		{
			_admin = admin;
		}





		public IActionResult Index()
		{
			return View();
		}
		
		
		public IActionResult User()
		{
			var userData=_admin.getUserList();
			AdminViewModel model = new AdminViewModel()
			{
				UserList = userData
			};

			return View(model);
		}

		[HttpGet]
		public IActionResult getDataForUser(long uId)
		{
           
			var data = _admin.getDataForUserPanel(uId);
            return Json(new
            {
                firstName = data.FirstName,
                lastName = data.LastName,
                email = data.Email,
                employeeId = data.EmployeeId,
                department = data.Department,
                status = data.Status,
                userId = uId

            });
        }

    }
}
