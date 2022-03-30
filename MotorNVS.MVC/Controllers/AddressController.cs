using Microsoft.AspNetCore.Mvc;
using MotorNVS.BL.DTOs.AddressDTO;
using MotorNVS.BL.Services;

namespace MotorNVS.MVC.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        public async Task<ActionResult> Index()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                List<AddressResponse> responses = new List<AddressResponse>();

                if (TempData["shortMessage"] != null)
                {
                    ViewBag.Message = TempData["shortMessage"];
                };

                try
                {
                    responses = await _addressService.GetAllAddresses();

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
        public async Task<ActionResult> Create(AddressRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _addressService.CreateAddress(req);

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
                    AddressResponse res = await _addressService.GetAddressById(id);

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
        public async Task<ActionResult> Edit(int id, AddressRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _addressService.UpdateAddress(id, req);

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
                AddressResponse res = await _addressService.GetAddressById(id);

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
