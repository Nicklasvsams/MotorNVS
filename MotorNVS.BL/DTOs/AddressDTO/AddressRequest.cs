using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.AddressDTO
{
    public class AddressRequest
    {
        [Required]
        [StringLength(100, ErrorMessage = "Street address can not be longer than 100 characters long")]
        [Display(Name = "Street name and number")]
        public string StreetAndNo { get; set; }
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Display(Name = "Zipcode ID")]
        [Required]
        public int ZipcodeId { get; set; }
    }
}
