using MotorNVS.BL.DTOs.AddressDTO;
using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.CustomerDTO
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        [Display(Name = "Address ID")]
        public int AddressId { get; set; }

        public AddressResponse AddressResponse { get; set; }
    }
}
