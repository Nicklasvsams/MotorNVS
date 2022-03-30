using Microsoft.AspNetCore.Mvc;
using MotorNVS.BL.DTOs.VehicleDTO;
using MotorNVS.BL.Services;

namespace MotorNVS.MVC.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        public async Task<ActionResult> Index()
        {
            List<VehicleResponse> responses = new List<VehicleResponse>();

            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"];
            };

            try
            {
                responses = await _vehicleService.GetAllVehicles();

                return View(responses);
            }
            catch
            {
                return View(responses);
            };
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(VehicleRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _vehicleService.CreateVehicle(req);

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
            try
            {
                VehicleResponse res = await _vehicleService.GetVehicleById(id);

                return View(res);
            }
            catch
            {
                TempData["shortMessage"] = "An error occured trying to fetch the entry, please try again.";

                return RedirectToAction(nameof(Index));
            };
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, VehicleRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _vehicleService.UpdateVehicle(id, req);

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
                VehicleResponse res = await _vehicleService.GetVehicleById(id);

                return View(res);
            }
            catch
            {
                TempData["shortMessage"] = "An error occured, please try again.";

                return RedirectToAction(nameof(Index));
            };
        }

        public async Task<ActionResult> Activation(int id)
        {
            try
            {
                await _vehicleService.VehicleActivation(id);

                TempData["shortMessage"] = "Status succesfully changed!";

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["shortMessage"] = "An error occured when trying to change the status of the entry, please try again.";

                return RedirectToAction(nameof(Index));
            };
        }
    }
}
