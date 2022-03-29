using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.CustomerDTO
{
    public class CustomerRequest
    {
        [Required]
        [StringLength(50, ErrorMessage = "First name can not be longer than 50 characters long")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Last name can not be longer than 50 characters long")]
        [Display(Name = "Last name")]
        public string? LastName { get; set; }
        [Required]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public bool IsActive { get; set; } = true;
        [Required]
        [Display(Name = "Address ID")]
        public int AddressId { get; set; }
    }
}
