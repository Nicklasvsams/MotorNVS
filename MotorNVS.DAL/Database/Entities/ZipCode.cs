using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorNVS.DAL.Database.Entities
{
    public class ZipCode
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(20)")]
        public string? ZipcodeNo { get; set; }
    }
}
