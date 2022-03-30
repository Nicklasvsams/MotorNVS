using MotorNVS.BL.DTOs.ZipcodeDTO;
using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.AddressDTO
{
    public class AddressResponse
    {
        public int Id { get; set; }
        [Display(Name = "Street name and number")]
        public string StreetAndNo { get; set; }
        public DateTime CreateDate { get; set; }
        [Display(Name = "Zipcode ID")]
        public int ZipcodeId { get; set; }

        public ZipcodeResponse ZipcodeResponse { get; set; }
    }
}
