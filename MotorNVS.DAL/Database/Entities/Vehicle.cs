using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorNVS.DAL.Database.Entities
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Make { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Model { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "int")]
        public int CategoryId { get; set; }
        [Column(TypeName = "int")]
        public int FuelId { get; set; }

        public Category Category { get; set; }
        public Fuel Fuel { get; set; }
    }
}
