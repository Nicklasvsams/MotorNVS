using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.AddressDTO
{
    public class AddressRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Street address can not be longer than 100 characters long")]
        public string StreetAndNo { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public int ZipcodeId { get; set; }
    }
}
