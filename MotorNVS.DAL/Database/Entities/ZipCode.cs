using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorNVS.DAL.Database.Entities
{
    public class Zipcode
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public string? ZipcodeNo { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string? City { get; set; }
    }
}
