using Microsoft.AspNetCore.Mvc;

namespace MotorNVS.MVC.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
