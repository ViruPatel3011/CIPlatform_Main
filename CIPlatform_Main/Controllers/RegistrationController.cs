using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CIPlatform_Main.Controllers
{
    public class RegistrationController : Controller
    {

        private readonly IRegistartionRepository _registartionRepository;

        public RegistrationController(IRegistartionRepository registartionRepository)
        {
            _registartionRepository = registartionRepository;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Registration(RegistrationVM registrationVM)
        {

            if (ModelState.IsValid)
            {
                var isValid = _registartionRepository.GetUsers(registrationVM);
                if (isValid)
                {
                    TempData["success"] = "Register is succesful.";
                    return RedirectToAction("Login", "Login");
                }
                else
                {
                    TempData["Error"] = "Email is Already Exist";
                    return RedirectToAction("Register", "Register");
                }
            }
            return View();
           
        }
    }
}
