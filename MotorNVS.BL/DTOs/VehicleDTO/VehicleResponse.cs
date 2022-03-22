using MotorNVS.BL.DTOs.CategoryDTO;
using MotorNVS.BL.DTOs.FuelDTO;

namespace MotorNVS.BL.DTOs.VehicleDTO
{
    public class VehicleResponse
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public DateTime CreateDate { get; set; }
        public int CategoryId { get; set; }
        public int FuelId { get; set; }

        public CategoryResponse CategoryResponse { get; set; }
        public FuelResponse FuelResponse { get; set; }
    }
}
