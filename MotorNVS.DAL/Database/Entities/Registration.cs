using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorNVS.DAL.Database.Entities
{
    public class Registration
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTimeOffset CreateDate { get; set; }
        [Column(TypeName = "int")]
        public int CustomerId { get; set; }
        [Column(TypeName = "int")]
        public int VehicleId { get; set; }

        public Customer Customer { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
