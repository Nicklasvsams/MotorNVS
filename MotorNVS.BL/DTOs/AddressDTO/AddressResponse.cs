using MotorNVS.BL.DTOs.ZipcodeDTO;

namespace MotorNVS.BL.DTOs.AddressDTO
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public string StreetAndNo { get; set; }
        public DateTime CreateDate { get; set; }
        public int ZipcodeId { get; set; }

        public ZipcodeResponse ZipcodeResponse { get; set; }
    }
}
