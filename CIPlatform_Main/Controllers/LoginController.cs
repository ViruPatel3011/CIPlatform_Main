using CIPlatform_Main.Entities.ViewModel;
using CIPlatform_Main.Repository.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using CIPlatform_Main.Entities.Data;
using CIPlatform_Main.Entities.Models;

namespace CIPlatform_Main.Controllers
{
    public class LoginController : Controller
    {



        private readonly ILoginRepository _loginRepository;
        private readonly CiPlatformContext _ciPlatformContext;

        public LoginController(ILoginRepository loginRepository, CiPlatformContext ciPlatformContext)
        {
            _loginRepository = loginRepository;
            _ciPlatformContext = ciPlatformContext;


        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Admin(LoginVM loginVM)
        {
            List<Admin> adminList = _loginRepository.getValidAdmin();
            var validAdmin = adminList.Where(x => x.Email == loginVM.Email);
            if (validAdmin != null)
            {
                if (ModelState.IsValid)
                {

                    var status = _ciPlatformContext.Admins.Where(x => x.Email == loginVM.Email && x.Password == loginVM.Password).FirstOrDefault();
                    if (status != null)
                    {

                        /*This ClaimsIdentity method provides user information automatically so applications do not need to request it of
                        the user and the user doesn't have to provide that information separately for different applications.*/
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, status.Email) },
                                CookieAuthenticationDefaults.AuthenticationScheme);
                        /*CookieAuthenticationDefaults.AuthenticationScheme provides “Cookies” for the scheme*/

                        identity.AddClaim(new Claim(ClaimTypes.Name, status.FirstName));
                        identity.AddClaim(new Claim(ClaimTypes.Surname, status.LastName));
                        identity.AddClaim(new Claim(ClaimTypes.Email, status.Email));
                        //identity.AddClaim(new Claim(ClaimTypes.Sid, Convert.ToString(status.UserId)));
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("EmailId", status.Email);

                        TempData["Success Message"] = "Successfully Login!";
                        return RedirectToAction("User", "Admin");

                    }



                }
            }


            TempData["Error Message"] = "Enter Valid username Or Password!";
            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM loginVM)
        {

            List<User> userList = _loginRepository.GetUser();
            var validUser = userList.Where(x => x.Email == loginVM.Email);
            if (validUser != null)
            {
                if (ModelState.IsValid)
                {
                    var status = _ciPlatformContext.Users.Where(x => x.Email == loginVM.Email && x.Password == loginVM.Password).FirstOrDefault();
                    if (status != null)
                    {

                        /*This ClaimsIdentity method provides user information automatically so applications do not need to request it of
                        the user and the user doesn't have to provide that information separately for different applications.*/
                        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, status.Email) },
                                CookieAuthenticationDefaults.AuthenticationScheme);
                        /*CookieAuthenticationDefaults.AuthenticationScheme provides “Cookies” for the scheme*/

                        identity.AddClaim(new Claim(ClaimTypes.Name, status.FirstName));
                        identity.AddClaim(new Claim(ClaimTypes.Surname, status.LastName));
                        identity.AddClaim(new Claim(ClaimTypes.Email, status.Email));
                        identity.AddClaim(new Claim(ClaimTypes.Sid, Convert.ToString(status.UserId)));
                        var principal = new ClaimsPrincipal(identity);
                        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                        HttpContext.Session.SetString("EmailId", status.Email);

                        TempData["Success Message"] = "Successfully Login!";
                        return RedirectToAction("LandingPage", "Home");

                    }


                }
            }


            TempData["Error Message"] = "Enter Valid username Or Password!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            TempData["Success Message"] = "LogOut Successfully";
            return RedirectToAction("LandingPage", "Home");
        }



        // policy page
        public IActionResult PolicyPage()
        {
            var userList = _loginRepository.GetUser();
            LandingPageVM landingPage = new LandingPageVM()
            {

                UserData = userList

            };

            return View(landingPage);
        }

        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ContactUs(UserViewModel userView)
        {
            var identity = User.Identity as ClaimsIdentity;
            var uid = identity?.FindFirst(ClaimTypes.Sid)?.Value;
            var addContactUsData = _loginRepository.AddContactUsData(userView, Convert.ToInt32(uid));
            if (addContactUsData)
            {
                TempData["Success Message"] = "Data Add Successfully";
                return RedirectToAction("UserProfile", "User");
            }
            else
            {

                TempData["Error Message"] = "Data not added!";
                return RedirectToAction("UserProfile", "User");
            }

        }

    }
}
