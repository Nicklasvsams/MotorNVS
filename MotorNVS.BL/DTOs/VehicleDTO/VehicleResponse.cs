using MotorNVS.BL.DTOs.CategoryDTO;
using MotorNVS.BL.DTOs.FuelDTO;
using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.VehicleDTO
{
    public class VehicleResponse
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime CreateDate { get; set; }
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }
        [Display(Name = "Fuel ID")]
        public int FuelId { get; set; }

        public CategoryResponse CategoryResponse { get; set; }
        public FuelResponse FuelResponse { get; set; }
    }
}
