using Microsoft.AspNetCore.Mvc;
using MotorNVS.BL.DTOs.CustomerDTO;
using MotorNVS.BL.Services;

namespace MotorNVS.MVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<ActionResult> Index()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                List<CustomerResponse> responses = new List<CustomerResponse>();

                if (TempData["shortMessage"] != null)
                {
                    ViewBag.Message = TempData["shortMessage"];
                };

                try
                {
                    responses = await _customerService.GetAllCustomers();

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
        public async Task<ActionResult> Create(CustomerRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _customerService.CreateCustomer(req);

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
                    CustomerResponse res = await _customerService.GetCustomerById(id);

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
        public async Task<ActionResult> Edit(int id, CustomerRequest req)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _customerService.UpdateCustomer(id, req);

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
                CustomerResponse res = await _customerService.GetCustomerById(id);

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
            if (HttpContext.Session.GetString("user") != null)
            {
                try
                {
                    await _customerService.CustomerActivation(id);

                    TempData["shortMessage"] = "Status succesfully changed!";

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["shortMessage"] = "An error occured when trying to change the status of the entry, please try again.";

                    return RedirectToAction(nameof(Index));
                };
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
