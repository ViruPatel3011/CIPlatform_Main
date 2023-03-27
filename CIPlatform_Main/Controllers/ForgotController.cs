using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using CIPlatform_Main.Repository.Repository;
using Microsoft.AspNetCore.Mvc;
using IActionResult = Microsoft.AspNetCore.Mvc.IActionResult;

namespace CIPlatform_Main.Controllers
{
    public class ForgotController : Controller
    {

        private readonly IForgotRepository _forgotRepository;

        public ForgotController(IForgotRepository forgotRepository)
        {
            _forgotRepository = forgotRepository;
        }



        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult Forgot()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Forgot(ForgotVM forgotVM)
        {
            if (ModelState.IsValid)
            {
                var emailIsValid = _forgotRepository.checkUser(forgotVM);
                //var validEmail = _ciPlatformContext.Users.FirstOrDefault(u => u.Email == tempForgotPassword.Email);
                if (emailIsValid)
                {
                    string url = Url.Action("ResetPassword", "ResetPassword", new { email = forgotVM.Email, token = "{token}" }, Request.Scheme);
                    var sendEmail = _forgotRepository.sendMail(forgotVM, url);

                    if (sendEmail)
                    {
                        TempData["success"] = "Mail Send Successfully";
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        TempData["Error"] = "Email Does Not Send";
                        return RedirectToAction("ForgotPassword", "ForgotPassword");
                    }
                }
                else
                {
                    TempData["Error"] = "Invalid Email";
                    return RedirectToAction("ForgotPassword", "ForgotPassword");
                }
            }
            return View();


        }
     
    }
}
