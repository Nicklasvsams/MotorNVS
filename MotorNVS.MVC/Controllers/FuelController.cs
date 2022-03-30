using Microsoft.AspNetCore.Mvc;
using MotorNVS.BL.DTOs.FuelDTO;
using MotorNVS.BL.Services;

namespace MotorNVS.MVC.Controllers
{
    public class FuelController : Controller
    {
        private readonly IFuelService _fuelService;

        public FuelController(IFuelService fuelService)
        {
            _fuelService = fuelService;
        }

        public async Task<ActionResult> Index()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                List<FuelResponse> responses = new List<FuelResponse>();

                if (TempData["shortMessage"] != null)
                {
                    ViewBag.Message = TempData["shortMessage"];
                };

                try
                {
                    responses = await _fuelService.GetAllFuels();

                    return View(responses);
                }
                catch
                {
                    return View(responses);
                };
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                return View();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FuelRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _fuelService.CreateFuel(req);

                    TempData["shortMessage"] = "Entry succesfully created!";

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ViewBag.Message = "An error occured, please try again.";

                    return View();
                };
            };

            return View();
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                try
                {
                    FuelResponse res = await _fuelService.GetFuelById(id);

                    return View(res);
                }
                catch
                {
                    TempData["shortMessage"] = "An error occured trying to fetch the entry, please try again.";

                    return RedirectToAction(nameof(Index));
                };
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, FuelRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _fuelService.UpdateFuel(id, req);

                    TempData["shortMessage"] = "Entry has been succesfully updated!";

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["shortMessage"] = "An error occured, please try again.";

                    return RedirectToAction(nameof(Index));
                };
            };

            try
            {
                FuelResponse res = await _fuelService.GetFuelById(id);

                return View(res);
            }
            catch
            {
                TempData["shortMessage"] = "An error occured, please try again.";

                return RedirectToAction(nameof(Index));
            };
        }
    }
}
