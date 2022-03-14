using MotorNVS.BL.DTOs.CustomerDTO;
using MotorNVS.BL.DTOs.VehicleDTO;

namespace MotorNVS.BL.DTOs.RegistrationDTO
{
    public class RegistrationResponse
    {
        public int Id { get; set; }
        public DateTime RegistrationDate { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }

        public CustomerResponse CustomerResponse { get; set; }
        public VehicleResponse VehicleResponse { get; set; }
    }
}
