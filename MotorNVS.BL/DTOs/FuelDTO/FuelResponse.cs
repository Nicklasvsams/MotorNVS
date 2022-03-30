using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.FuelDTO
{
    public class FuelResponse
    {
        public int Id { get; set; }
        [Display(Name = "Fuel name")]
        public string FuelName { get; set; }
    }
}
