using MotorNVS.BL.DTOs.RegistrationDTO;

namespace MotorNVS.MVC.Models
{
    public class RegistrationListViewModel
    {
        public List<RegistrationResponse> List { get; set; } = new List<RegistrationResponse>();
    }
}