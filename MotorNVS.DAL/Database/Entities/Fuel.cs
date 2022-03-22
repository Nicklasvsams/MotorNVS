using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorNVS.DAL.Database.Entities
{
    public class Fuel
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string FuelName { get; set; }
    }
}
