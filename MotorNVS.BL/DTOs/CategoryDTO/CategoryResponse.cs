using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.CategoryDTO
{
    public class CategoryResponse
    {
        public int Id { get; set; }
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }
    }
}
