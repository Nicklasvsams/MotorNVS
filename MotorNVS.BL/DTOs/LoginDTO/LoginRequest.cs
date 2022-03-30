using System.ComponentModel.DataAnnotations;

namespace MotorNVS.BL.DTOs.LoginDTO
{
    public class LoginRequest
    {
        [Required]
        [StringLength(20, ErrorMessage = "Username can not be longer than 20 characters long")]
        public string Username { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Password can not be longer than 100 characters long")]
        public string Password { get; set; }
    }
}
