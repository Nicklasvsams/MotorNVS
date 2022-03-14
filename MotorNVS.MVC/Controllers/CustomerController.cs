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

        public async Task<IActionResult> Index()
        {
            CustomerResponse customer = await _customerService.GetCustomerById(2);

            if (customer != null)
            {
                ViewBag.CustomerResponse = customer;
            }

            return View();
        }
    }
}
