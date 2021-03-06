using MotorNVS.BL.DTOs.CustomerDTO;
using MotorNVS.BL.DTOs.VehicleDTO;
using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.RegistrationDTO
{
    public class RegistrationResponse
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }
        [Display(Name = "Vehicle ID")]
        public int VehicleId { get; set; }

        public CustomerResponse CustomerResponse { get; set; }
        public VehicleResponse VehicleResponse { get; set; }
    }
}
