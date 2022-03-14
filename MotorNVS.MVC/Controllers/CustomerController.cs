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
            List<CustomerResponse> customers = await _customerService.GetAllCustomers();

            ViewBag.CustomerResponses = customers;

            return View();
        }
    }
}
