using MotorNVS.BL.DTOs.RegistrationDTO;
using System.ComponentModel.DataAnnotations;

namespace MotorNVS.MVC.Models
{
    public class RegistrationIndexViewModel
    {
        [Display(Name = "Search for registration by vehicle ID")]
        public int SearchInt { get; set; }
        public List<RegistrationResponse> RegList { get; set; }
    }
}
