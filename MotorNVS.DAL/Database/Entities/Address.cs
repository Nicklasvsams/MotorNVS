using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MotorNVS.DAL.Database.Entities
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string StreetAndNo { get; set; }
        [Column(TypeName = "date")]
        public DateTime CreateDate { get; set; }
        [Column(TypeName = "int")]
        public int ZipCodeId { get; set; }

        public Zipcode Zipcode { get; set; }
    }
}
