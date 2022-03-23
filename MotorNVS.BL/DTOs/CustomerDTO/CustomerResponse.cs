using MotorNVS.BL.DTOs.AddressDTO;

namespace MotorNVS.BL.DTOs.CustomerDTO
{
    public class CustomerResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
        public int AddressId { get; set; }

        public AddressResponse AddressResponse { get; set; }
    }
}
