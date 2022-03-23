using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.RegistrationDTO
{
    public class RegistrationRequest
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
    }
}
