using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform_Main.Controllers
{
    public class ResetPasswordController : Controller
    {


        private readonly IResetPasswordRepository _resetPasswordRepository;

        public ResetPasswordController(IResetPasswordRepository resetPasswordRepository)
        {
            _resetPasswordRepository = resetPasswordRepository;
        }


        [HttpGet]
        public IActionResult ResetPassword(string Email, string token)

        {
            var isEmailValid=_resetPasswordRepository.ValidEmail(Email, token);

            if(isEmailValid)
            {
                var resetPassword = new ResetPasswordVM()
                {
                    Token = token,
                    Email = Email,
                };
                return View(resetPassword);
            }
            else
            {
                TempData["Error"] = "Email is Invalid";
                return View();
            }
        }


        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordVM resetPasswordVM)
        {
          var validUser=_resetPasswordRepository.ValidUser(resetPasswordVM);
            if(validUser)
            {
                TempData["Success"] = "Password reset successfully";
                return RedirectToAction("Login", "Login");
            }
            else
            {
                TempData["Error"] = "Invalid User";
                return View();
            }
        }

         public IActionResult Index()
                {
                    return View();
                }
            }
}
