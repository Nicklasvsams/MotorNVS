using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.FuelDTO
{
    public class FuelRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Fuel name can not be longer than 50 characters long")]
        public string FuelName { get; set; }
    }
}
