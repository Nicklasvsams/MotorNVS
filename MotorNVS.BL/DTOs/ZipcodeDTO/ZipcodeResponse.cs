using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.ZipcodeDTO
{
    public class ZipcodeResponse
    {
        public int Id { get; set; }
        [Display(Name = "Zipcode number")]
        public string ZipcodeNo { get; set; }
        public string City { get; set; }
    }
}
