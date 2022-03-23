using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.VehicleDTO
{
    public class VehicleRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Make can not be longer than 50 characters long")]
        public string Make { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Model can not be longer than 50 characters long")]
        public string Model { get; set; }
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int FuelId { get; set; }
    }
}
