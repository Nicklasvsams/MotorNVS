using Microsoft.AspNetCore.Mvc;
using MotorNVS.BL.DTOs.ZipcodeDTO;
using MotorNVS.BL.Services;

namespace MotorNVS.MVC.Controllers
{
    public class ZipcodeController : Controller
    {
        private readonly IZipcodeService _zipcodeService;

        public ZipcodeController(IZipcodeService zipcodeService)
        {
            _zipcodeService = zipcodeService;
        }

        public async Task<ActionResult> Index()
        {
            List<ZipcodeResponse> responses = new List<ZipcodeResponse>();

            if (TempData["shortMessage"] != null)
            {
                ViewBag.Message = TempData["shortMessage"];
            };

            try
            {
                responses = await _zipcodeService.GetAllZipcodes();

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
        public async Task<ActionResult> Create(ZipcodeRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _zipcodeService.CreateZipcode(req);

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
                ZipcodeResponse res = await _zipcodeService.GetZipcodeById(id);

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
        public async Task<ActionResult> Edit(int id, ZipcodeRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _zipcodeService.UpdateZipcode(id, req);

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
                ZipcodeResponse res = await _zipcodeService.GetZipcodeById(id);

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
