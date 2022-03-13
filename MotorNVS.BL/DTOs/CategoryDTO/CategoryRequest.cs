using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.CategoryDTO
{
    public class CategoryRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "Fuel name can not be longer than 50 characters long")]
        public string CategoryName { get; set; }
    }
}
