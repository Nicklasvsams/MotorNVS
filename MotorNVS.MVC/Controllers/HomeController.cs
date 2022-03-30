using Microsoft.AspNetCore.Mvc;
using MotorNVS.BL.DTOs.LoginDTO;
using MotorNVS.BL.Services;
using MotorNVS.MVC.Models;
using System.Diagnostics;

namespace MotorNVS.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService _loginService;

        public HomeController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public IActionResult Index()
        {
            if(TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"];
            }

            if(HttpContext.Session.GetString("user") != null)
            {
                ViewBag.User = HttpContext.Session.GetString("user");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                LoginResponse res = await _loginService.AuthorizeLogin(username, password);

                if (res.LoginAuthorized)
                {
                    TempData["shortMessage"] = "Login successful";
                    HttpContext.Session.SetString("user", username);
                }
                else
                {
                    TempData["shortMessage"] = "Credentials didn't match, try again";
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["shortMessage"] = "Login failed, try again";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Logout()
        {
            try
            {
                if (HttpContext.Session.GetString("user") != null)
                {
                    TempData["shortMessage"] = "Logout successful";
                    HttpContext.Session.Remove("user");
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["shortMessage"] = "An error occured";
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}