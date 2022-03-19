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

        // GET: RegistrationController
        public async Task<ActionResult> Index()
        {
            List<RegistrationResponse> registrations = new List<RegistrationResponse>();

            try
            {
                registrations = await _registrationService.GetAllRegistrations();

                return View(registrations);
            }
            catch
            {
                return View(registrations);
            }
        }

        // GET: RegistrationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RegistrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RegistrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegistrationRequest registration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _registrationService.CreateRegistration(registration);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }

        // GET: RegistrationController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                RegistrationResponse reg = await _registrationService.GetRegistrationById(id);

                return View(reg);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: RegistrationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, RegistrationRequest registration)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    RegistrationResponse reg = await _registrationService.UpdateRegistration(id, registration);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {

                    return RedirectToAction(nameof(Index));
                }
            }
            try
            {
                RegistrationResponse errorReg = await _registrationService.GetRegistrationById(id);
                return View(errorReg);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: RegistrationController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _registrationService.DeleteRegistrationById(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
