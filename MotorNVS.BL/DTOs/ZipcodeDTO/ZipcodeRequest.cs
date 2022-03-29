using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.ZipcodeDTO
{
    public class ZipcodeRequest
    {
        [Required]
        [StringLength(20, ErrorMessage = "Zipcode can not be longer than 20 characters long")]
        [Display(Name = "Zipcode number")]
        public string ZipcodeNo { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "City can not be longer than 50 characters long")]
        public string City { get; set; }
    }
}
