using Microsoft.AspNetCore.Mvc;
using MotorNVS.BL.DTOs.RegistrationDTO;
using MotorNVS.BL.Services;
using MotorNVS.MVC.Models;

namespace MotorNVS.MVC.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        public async Task<ActionResult> Index()
        {
            if(HttpContext.Session.GetString("user") != null)
            {
                RegistrationIndexViewModel resModel = new RegistrationIndexViewModel();

                if (TempData["shortMessage"] != null)
                {
                    ViewBag.Message = TempData["shortMessage"];
                };

                try
                {
                    resModel.RegList = await _registrationService.GetAllRegistrations();

                    return View(resModel);
                }
                catch
                {
                    return View(resModel);
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
        public async Task<ActionResult> Create(RegistrationRequest registration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _registrationService.CreateRegistration(registration);

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
                    RegistrationResponse res = await _registrationService.GetRegistrationById(id);

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
        public async Task<ActionResult> Edit(int id, RegistrationRequest registration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _registrationService.UpdateRegistration(id, registration);

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
                RegistrationResponse res = await _registrationService.GetRegistrationById(id);

                return View(res);
            }
            catch
            {
                TempData["shortMessage"] = "An error occured, please try again.";

                return RedirectToAction(nameof(Index));
            };
        }

        public async Task<ActionResult> Delete(int id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                try
                {
                    await _registrationService.DeleteRegistrationById(id);

                    TempData["shortMessage"] = "Entry successfully deleted!";

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["shortMessage"] = "An error occured when trying to delete the entry, please try again.";

                    return RedirectToAction(nameof(Index));
                };
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(RegistrationIndexViewModel regModelForID)
        {
            RegistrationIndexViewModel regModel = new RegistrationIndexViewModel();

            if (HttpContext.Session.GetString("user") != null)
            {
                try
                {
                    List<RegistrationResponse> resList = await _registrationService.GetAllRegistrationsByVehicleId(regModelForID.SearchInt);
                    regModel.RegList = resList;

                    return View(regModel);
                }
                catch
                {
                    TempData["shortMessage"] = "An error occured trying to fetch the entries, please try again.";

                    return RedirectToAction(nameof(Index));
                };
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
