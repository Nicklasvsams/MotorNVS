using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.CustomerDTO
{
    public class CustomerRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "First name can not be longer than 50 characters long")]
        public string FirstName { get; set; }
        [StringLength(50, ErrorMessage = "Last name can not be longer than 50 characters long")]
        public string? LastName { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public int AddressId { get; set; }
    }
}
